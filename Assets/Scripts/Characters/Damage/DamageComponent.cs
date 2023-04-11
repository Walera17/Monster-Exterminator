using UnityEngine;

namespace Characters.Damage
{
    public abstract class DamageComponent : MonoBehaviour, ITeamInterface
    {
        [SerializeField] private bool damageFriendly;
        [SerializeField] private bool damageEnemy;
        [SerializeField] private bool damageNeutral;

        private ITeamInterface teamInterface;

        public int GetTeamID()
        {
            if (teamInterface != null)
                return teamInterface.GetTeamID();
            return -1;
        }

        public void SetTeamInterface(ITeamInterface teamInterfaceParam)
        {
            teamInterface = teamInterfaceParam;
        }

        public bool ShouldDamage(GameObject other)
        {
            if (teamInterface == null) return false;

            TeamRelation relation = teamInterface.GetRelationTowards(other);

            if (damageFriendly && relation == TeamRelation.Friendly)
                return true;
            if (damageEnemy && relation == TeamRelation.Enemy)
                return true;
            if (damageNeutral && relation == TeamRelation.Neutral)
                return true;

            return false;
        }
    }
}