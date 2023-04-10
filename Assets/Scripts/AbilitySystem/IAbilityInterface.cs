namespace AbilitySystem
{
    public interface IAbilityInterface
    {
        void AddMoveSpeed(float boostAmt);

        void HealthRegenerate(float healthRegenerateAmount, float speedRegenerate);

        public float GetDeltaHealth(float healthRegenerateAmount);
    }
}