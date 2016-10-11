using System;
using System.Collections.Generic;
using System.Linq;

namespace Guidelines.Model.Links
{
    /// <summary>
    ///     Default functionality to manage guideline links.
    /// </summary>
    /// <seealso cref="Guidelines.Model.Links.IGuidelineLinkManager" />
    public class DefaultGuidelineLinkManager : IGuidelineLinkManager
    {
        private Dictionary<string, IIdentifiableGuidelineEntity> _guidelineEntities = new Dictionary<string, IIdentifiableGuidelineEntity>();

        /// <summary>
        ///     Registers the specified entity.
        /// </summary>
        /// <param name="entity">The entity that should be registered.</param>
        public void Register(IIdentifiableGuidelineEntity entity)
        {
            // Check if entity identifier has already been registered.
            if (_guidelineEntities.Any(pair => pair.Key.ToLower() == entity.Identifier.ToLower()))
            {
                throw new Exception("The specified entity already exists within the link manager.")
                {
                    Source = nameof(entity)
                };
            }

            // Add the entity to the link dictionary.
            _guidelineEntities?.Add(entity?.Identifier, entity);
        }

        /// <summary>
        ///     Obtains the entity with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the entity that should be returned.</param>
        /// <returns>Returns the entity with the specified identifier.</returns>
        public IIdentifiableGuidelineEntity ObtainEntity(string identifier)
        {
            var entityPair = _guidelineEntities.SingleOrDefault(pair => pair.Key.ToLower() == identifier?.ToLower());
            return entityPair.Value;
        }
    }
}