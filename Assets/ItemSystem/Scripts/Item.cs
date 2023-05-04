using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField]
	GameObject itemPrefab;
	[SerializeField]
	Sprite icon;

	[SerializeField]
	string itemName;
	[SerializeField]
	[TextArea(4, 16)]
	string description;

	[SerializeField]
	float weight = 0;
	[SerializeField]
	int quantity = 1;
	[SerializeField]
	int maxStackableQuantity = 1; // for bundles of items, such as arrows or coins
	[SerializeField]
	int pointValue = 1; // for scoring

	[SerializeField]
	bool isStorable = false; // if false, item will be used on pickup
	[SerializeField]
	bool isConsumable = true; // if true, item will be destroyed (or quantity reduced) when used

	[SerializeField]
	bool isPickupOnCollision = false;

	private void Start()
	{
		if (isPickupOnCollision)
		{
			gameObject.GetComponent<Collider>().isTrigger = true;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (isPickupOnCollision)
		{
			if (collider.tag == "Player")
			{
				Interact();
			}
		}
	}

	public void Interact()
	{
		Debug.Log("Picked up " + transform.name);
		AudioManager.instance.Play("CoinCollect");

		if (isStorable)
		{
			Store();
		}
		else
		{
			Use();
		}
	}

	void Store()
	{
		Debug.Log("Storing " + transform.name);

		// TODO Inventory system

		Destroy(gameObject);
	}

	void Use()
	{
		Debug.Log("Using " + transform.name);
		if (isConsumable)
		{
			quantity--;
			if (quantity <= 0)
			{
				Destroy(gameObject);
			}

			GameManager.IncrementScore(pointValue);
		}
	}
}