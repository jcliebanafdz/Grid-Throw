﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int Speed;
	public int Life;
	public int Damage;
	public int maxCellsMovement;
	public bool alreadyMoved;
	public bool Alive = false;

	public UnityEngine.UI.Slider EnemyHealth;

	public void RecieveDamage(int Damage)
	{
		if(Life - Damage <= 0)
		{
			var tile = ActionPhaseManager.Instance._MapGenerator.Tiles.Find(_tile => _tile.xCoord == transform.position.x && _tile.yCoord == transform.position.z);
			
			if(tile != null)
			{
				//tile.isOccupiedByEnemy = false;
				tile.walkable = true;
			}
			
			MainManager.Instance._ActionPhaseManager._EnemyController.EnemyKilled(this);
			MainManager.Instance._PoolingManager.DespawnEnemy(this);
		}

		Life -= Damage;
		EnemyHealth.value = Life;		
	}

	public void Kill()
	{
		Alive = false;
		gameObject.SetActive(false);
		EnemyHealth.transform.parent.gameObject.SetActive(false);
	}

	public void Revive(EnemyInfo enemyInfo)
	{
		Speed = enemyInfo.Speed;
		Life = enemyInfo.Life;
		Damage = enemyInfo.Damage;
		maxCellsMovement = enemyInfo.Movements;

		EnemyHealth.maxValue = Life;
		EnemyHealth.value = Life;
		alreadyMoved = false;

		Alive = true;

		gameObject.SetActive(true);
		EnemyHealth.transform.parent.gameObject.SetActive(true);
	}
}
