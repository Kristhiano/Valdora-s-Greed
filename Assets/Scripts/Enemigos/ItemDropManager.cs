using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    public GameObject itemPrefab; // Prefab del ítem a dropear
    public int maxDrops; // Límite máximo de este ítem
}

public class ItemDropManager : MonoBehaviour
{
    public ItemDrop[] itemDrops; // Array de ítems que puede dropear el enemigo

    public void DropItems(Vector3 position)
    {
        foreach (var itemDrop in itemDrops)
        {
            // Generar un número aleatorio de ítems a dropear entre 0 y el máximo permitido
            int dropsToSpawn = Random.Range(0, itemDrop.maxDrops + 1);

            for (int i = 0; i < dropsToSpawn; i++)
            {
                // Establecer una posición aleatoria alrededor del enemigo
                Vector3 dropPosition = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                // Instanciar el ítem en la posición calculada
                Instantiate(itemDrop.itemPrefab, dropPosition, Quaternion.identity);
            }
        }
    }
}
