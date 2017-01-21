using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType{
	NONE = 0,
	PRISM = 1,
	BRASS = 2
}

public class GameState{

	private int _nbCadran;
	private int _nbZone;

	//1: cadran; 2: ligne(zone); 3: emplacement
	private BuildingType[,,] _buildings;
	//1: cadran; 2: zone // true: active; false: capturée
	private bool[,] _zones;

	public GameState (int nbCadran, int nbZone){
		_nbCadran = nbCadran;
		_nbZone = nbZone;
		_buildings = new BuildingType[_nbCadran,_nbZone,_nbZone];
		_zones = new bool[_nbCadran, _nbZone];

		for (int i = 0; i < _nbCadran-1; i++) {
			for (int j = 0; j< _nbZone; j++) {
				for (int k = 0; k < _nbZone; k++) {
					_buildings[i, j, k] = BuildingType.NONE;
				}
			} 
		}

		for (int i = 0; i < _nbCadran-1; i++) {
			for (int j = 0; j< _nbZone; j++) {
				_zones [i, j] = true;
			} 
		}

			
	}

	public void setBuilding(int cadran, int ligne, int emplacement, BuildingType building){
		_buildings [cadran, ligne, emplacement] = building;
	}

	public BuildingType getBuilding(int cadran, int ligne, int emplacement){
		return _buildings [cadran, ligne, emplacement];
	}

	public void destroyBuilding(int cadran, int ligne, int emplacement){
		setBuilding (cadran, ligne, emplacement, BuildingType.NONE);
	}

	public void captureZone(int cadran, int zone){
		_zones [cadran, zone] = false;
	}

	public bool isZoneActive(int cadran, int zone){
		return _zones [cadran, zone];
	}

	public void rotateBuildingsAnticlockwise(){
		BuildingType[,] buffer = new BuildingType [_nbZone, _nbZone];


		//sauvegarde du premier cadran dans le buffer
		for (int i = 0; i < _nbZone; i++) {
			for (int j = 0; j < _nbZone; j++) {
				buffer[i, j] = _buildings [0, i, j];
			}
		}

		//on switch tout les cadran, sauf le premier et le dernier
		for (int i = 0; i < _nbCadran-1; i++) {
			for (int j = 0; j< _nbZone; j++) {
				for (int k = 0; k < _nbZone; k++) {
					_buildings[i, j, k] = _buildings [i+1, j, k];
				}
			} 
		}

		//on met le premier dans le dernier grace au buffer
		for (int i = 0; i < _nbZone; i++) {
			for (int j = 0; j < _nbZone; j++) {
				_buildings [_nbCadran-1, i, j] = buffer[i, j];
			}
		}
	}

	public void rotateBuildingsClockwise(){
		BuildingType[,] buffer = new BuildingType [_nbZone, _nbZone];


		//sauvegarde du dernier cadran dans le buffer
		for (int i = 0; i < _nbZone; i++) {
			for (int j = 0; j < _nbZone; j++) {
				
				buffer[i, j] = _buildings [_nbCadran-1, i, j];
			}
		}

		//on switch tout les cadran, sauf le premier et le dernier
		for (int i = _nbCadran-1; i > 0; i--) {
			for (int j = 0; j< _nbZone; j++) {
				for (int k = 0; k < _nbZone; k++) {
					_buildings[i, j, k] = _buildings [i-1, j, k];
				}
			} 
		}

		//on met le dernier dans le premier grace au buffer
		for (int i = 0; i < _nbZone; i++) {
			for (int j = 0; j < _nbZone; j++) {
				_buildings [0, i, j] = buffer[i, j];
			}
		}
	}

	public int getNbCadran(){
		return _nbCadran;
	}

	public int getNbZone(){
		return _nbZone;
	}
}

