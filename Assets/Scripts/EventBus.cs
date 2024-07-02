using System;

public static class EventBus
{
    public static Action OnPlayerShot;
    public static Action<int, BodyPartType>  OnPlayerTakeDamage;
    public static Action<int> OnEnemyTakeDamage;
    public static Action OnEnemyDead;
    public static Action OnPlayerDead;
    public static Action OnDataLoaded;
    public static Action<int> OnPlayerHeal; 
    public static Action<ClothesItem> OnPlayerEquipArmor;
    public static Action<EquipmentSlot> OnPlayerUnEquipArmor;
}
