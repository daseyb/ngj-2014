using UnityEngine;
using System.Collections;

public class ObjectSpawner : OnPlayBase 
{
	public Object Block;

	public int SpawnRythm = 2;

	int playCounter = 0;

	protected override void OnStart ()
	{
		base.OnStart ();
	}

	protected override void OnPlay ()
	{
		base.OnPlay ();
		playCounter = (playCounter + 1) % SpawnRythm;
		if(playCounter == 0)
			Spawn ();
	}

	void Spawn() {
		float spawnX = 0;
		float spawnY = 0;

		switch (Random.Range(0, 5))
		{
		case 0: //Left
			spawnX = 0;
			spawnY = Random.Range(0, Camera.main.pixelHeight);
			break;
		case 1: //Right
			spawnX = Camera.main.pixelWidth;
			spawnY = Random.Range(0, Camera.main.pixelHeight);
			break;
		case 2: //Top
			spawnX = Random.Range(0, Camera.main.pixelWidth);
			spawnY = Camera.main.pixelHeight;
			break;
		case 4: //Bottom
			spawnX = Random.Range(0, Camera.main.pixelWidth);
			spawnY = 0;
			break;
		}
		
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(spawnX, spawnY, 0));
		position.z = 0;
		
		GameObject obj = (GameObject)Instantiate(Block, position, Quaternion.identity);
		Block2D block = obj.GetComponent<Block2D>();
		block.center = this.transform;
		block.Player = Player;
		
		obj.GetComponent<ColoredBlock>().SetColor((GameColor)Random.Range(0, 6));
	}
}
