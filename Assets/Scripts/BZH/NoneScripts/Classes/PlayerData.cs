using DataCs;
using UnityEngine;

public enum PlayerHpState : uint
{
	Dead = 0u,					//死亡
	Freezing = 1u,				//热量不足(1状态)
	Normal = 1u << 1,			//正常(2状态)
	Fever = 1u << 2,			//发热(3状态)
	Overheating = 1u << 3,		//过热(4状态)
}
    
public class PlayerData
{
	public float MaxHp { get; private set; }
	public float InitialHp { get; private set; }
	public float InitialDefense { get; private set; }
	public float NormalDamage { get; private set; }
	public float FeverDamage { get; private set; }
	public float OverheatingDamage { get; private set; }
	public float InitialMoveSpeed { get; private set; }
    
	public PlayerData(float mHp, float iDefense, float nDmg, float fDmg, float oDmg, float iSpeed)
	{
		MaxHp = mHp;
		InitialHp = 0.5f * MaxHp;
		InitialDefense = iDefense;
		NormalDamage = nDmg;
		FeverDamage = fDmg;
		OverheatingDamage = oDmg;
		InitialMoveSpeed = iSpeed;
	}

	public static PlayerData GetDefaultObject()
	{
		return new PlayerData
		(
			570.0f,
			0.0f,
			72.0f,
			32.0f,
			75.0f,
			8.0f
		);
	}
    
	/// <summary>
	/// 获取当前玩家状态。
	/// </summary>
	/// <param name="currHp">当前HP</param>
	/// <returns>HP对应的状态枚举值</returns>
	public PlayerHpState GetHpState(float currHp)
	{
		float hpPercentage = currHp / MaxHp;
		uint result = 0u;
		float[] kvps = Data_Empyrean.GetDefaultObject().PlayerStateChangedKvps;
		do
		{
			if (hpPercentage == 0.0f) break;
			result <<= 1;
			if (hpPercentage < kvps[0]) break;
			result <<= 1;
			if (hpPercentage < kvps[1]) break;
			result <<= 1;
			if (hpPercentage < kvps[2]) break;
			result <<= 1;
    
		} while (false);
    
		return (PlayerHpState)result;
	}
    
	/// <summary>
	/// 应用伤害。此函数仅具备计算性，不更改实际数值。
	/// </summary>
	/// <param name="dmg">原始伤害值</param>
	/// <param name="currHp">当前HP</param>
	/// <param name="currDefense">当前防御力</param>
	/// <param name="damageTaker">伤害施加者，为怪物属性对象</param>
	/// <returns>实际应造成的伤害</returns>
	public float ApplyDamage(float dmg, float currHp, float currDefense, MonsterData damageTaker)
	{
		float actuallyCaused = dmg * (1.0f - currDefense);
		if (GetHpState(currHp) == PlayerHpState.Overheating)
		{
			var data = Data_Empyrean.GetDefaultObject();
			actuallyCaused *=
				Random.Range(data.MinDamageIncreaseWhenOverheating, data.MaxDamageIncreaseWhenOverheating);
		}
    
		actuallyCaused = Mathf.Clamp(actuallyCaused, currHp, 9999.0f);
		return actuallyCaused;
	}
}