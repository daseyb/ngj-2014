using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour 
{
	public Object Block;
	public void Start () 
	{
		StartCoroutine ("Spawner");
	}

	IEnumerator Spawner()
	{
        float spawnX = 0;
        float spawnY = 0;
		while(true)
		{
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

			obj.GetComponent<ColoredBlock>().SetColor((GameColor)Random.Range(0, 6));

			yield return new WaitForSeconds(Random.Range(0.8f, 1.0f));
		}
	}
}
