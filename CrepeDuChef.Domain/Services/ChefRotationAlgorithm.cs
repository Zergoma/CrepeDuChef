using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.Domain.Extensions;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Domain.ValueObjects;

namespace CrepeDuChef.Domain.Services
{
    public class ChefRotationAlgorithm
    {
        public static ChefSelection SelectNextChef(
            List<User> allChefs,
            int lastSessionId,
            List<CrepesParty> sessionCrepes,
            IRandomProvider random,
            List<User>? availableChefs = null)
        {
            ArgumentNullException.ThrowIfNull(allChefs, nameof(allChefs));
            ArgumentNullException.ThrowIfNull(sessionCrepes, nameof(sessionCrepes));
            ArgumentNullException.ThrowIfNull(random, nameof(random));
            
            // no chefs att all
            if (allChefs.Count == 0)
            {
                throw new NoChefException();
            }

            // only one chef
            // always him, and incr session number
            if (allChefs.Count == 1)
            {
                return new ChefSelection(allChefs[0], lastSessionId + 1);
            }

            // juste based on all chefs, they are all available
            if (availableChefs is null ||
                availableChefs.Count == 0 || 
                availableChefs.Count == allChefs.Count)
            {
                return NormalSelection(allChefs, lastSessionId, sessionCrepes, random);
            }

            // special case:
            // one or more chefs are not available
            return WithAbsentSelection(allChefs, lastSessionId, sessionCrepes, availableChefs, random);
        }

        private static ChefSelection NormalSelection(
            List<User> allChefs,
            int lastSessionId,
            List<CrepesParty> sessionCrepes,
            IRandomProvider random)
        {
            // This function is private and checkd for good use but
            // In case of evolution -> argument vérification : should chef.Count >= 2
            ArgumentOutOfRangeException.ThrowIfLessThan(allChefs.Count, 2, nameof(allChefs));
            
            // RULE
            // chef number == session number
            bool sessionCompleted = sessionCrepes.Count == allChefs.Count;

            if (sessionCompleted)
            {
                // get previous session last chef
                Guid lastChefId = sessionCrepes.Last().UserId;

                // get remaining chef
                var remainingChefs = allChefs.Where(c => c.Id != lastChefId);

                // That's a new session
                int newSession = lastSessionId + 1;

                return new ChefSelection(remainingChefs.PickRandom(random), newSession);
            }

            // Session not yet completed
            // Optimization : get session chefs id
            HashSet<Guid> sessionChefIds = [.. sessionCrepes.Select(c => c.UserId) ];

            var remgChefs =
                allChefs.Where(c => !sessionChefIds.Contains(c.Id));
            
            // get one of the remaining
            return new ChefSelection(remgChefs.PickRandom(random), lastSessionId);
        }

        private static ChefSelection WithAbsentSelection(
            List<User> allChefs,
            int lastSessionId,
            List<CrepesParty> sessionCrepes,
            List<User> availableChefs,
            IRandomProvider random)
        {
            // Just one chef available
            // Use this one, no matter the last session, this is the only one
            if (availableChefs.Count == 1)
            {
                var user = allChefs.Single(c => c.Id == availableChefs.First().Id);
                return new ChefSelection(user, lastSessionId + 1);
            }

            // More than one in available list
            return WithAbsentSelectionRules(allChefs, lastSessionId, sessionCrepes, availableChefs, random);
        }

        private static ChefSelection WithAbsentSelectionRules(
            List<User> allChefs,
            int lastSessionId,
            List<CrepesParty> sessionCrepes,
            List<User> availableChefs,
            IRandomProvider random)
        {
            HashSet<Guid> availableChefIds = [.. availableChefs.Select(c => c.Id)];
            HashSet<Guid> sessionChefIds = [.. sessionCrepes.Select(cp => cp.UserId)];

            // Filter already used chefs during this session
            HashSet<Guid> remaining = [.. availableChefIds.Except(sessionChefIds)];

            // Just one, that our chef
            if (remaining.Count == 1)
            {
                return new ChefSelection(allChefs.Single(c => c.Id == remaining.First()), lastSessionId);
            }
            
            // 0 or >1
            HashSet<Guid> workOn = remaining.Count switch
            {
                0 => availableChefIds,  // no chef remaining -> use the full available list 
                _ => remaining          // at least 2
            };

            int sessionNumber =
                (remaining.Count == 0)  // we need a new session
                ? lastSessionId + 1
                : lastSessionId;        // still in a session

            Guid lastChefId = sessionCrepes.Last().UserId;

            var candidates = workOn.Except([lastChefId]);

            if (!candidates.Any())
            {
                throw new NoChefSelectionException();
            }

            if (candidates.Count() == 1)
            {
                return new ChefSelection(allChefs.Single(c => c.Id == candidates.First()), sessionNumber);
            }

            return new ChefSelection(allChefs.Single(c => c.Id == candidates.PickRandom(random)), sessionNumber);
        }
    }
}
