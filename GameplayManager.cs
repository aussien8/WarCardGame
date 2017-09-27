using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

	// For use with the Fisher-Yates Shuffle algorithm
	public Random rand = new Random();

	public List<Card> gameDeck = new List<Card> ();
	public Queue<Card> prizePool = new Queue<Card>();
	public Queue<Card> player = new Queue<Card>();
	public Queue<Card> computer = new Queue<Card>();

	public Canvas c;

	void Start() {
		Reset ();
	}

	void Update() {
		CheckVictory ();
	}

	public void CheckVictory() {
		c.GetComponent<GraphicRaycaster> ().enabled = false;
		if (player.Count == 0) {
			
			Debug.Log ("Computer won.");
		} else if (computer.Count == 0) {

			Debug.Log ("Player won.");
		}

		c.GetComponent<GraphicRaycaster> ().enabled = true;

	}

	public void Reset() {
		GenDeck ();
		//PrintDeck ();
		ShuffleDeck ();
		//PrintDeck ();
		SplitDeck ();
		c.GetComponent<GraphicRaycaster> ().enabled = true;
		//Debug.Log (player.Count);
		//Debug.Log (computer.Count);
	}

	// For debugging purposes
	public void PrintDeck() {
		foreach (Card c in gameDeck) {
			c.PrintCard ();
		}

	}

	// Generate each suit and populate the deck
	public void GenDeck() {
		gameDeck.Clear ();
		GenSuits ();

		/*
		GenDiamonds ();
		GenHearts ();
		GenSpades ();
		GenClubs ();
		*/
	}

	// Base generate version for game without graphics
	public void GenSuits() {
		// 1 - Diamonds, 2 - Hearts, 3 - Spades, 4 - Clubs
		for (int i = 1; i < 5; i++) {
			for (int j = 1; j < 14; j++) {
				Card newCard = new Card (j, i);
				gameDeck.Add (newCard);
			}
		}
	}


	// Create each suit of cards and put them in the list
	public void GenDiamonds() {
		// Suit identifier of 1 for diamonds
		for (int i = 1; i < 14; i++) {
			Card newCard = new Card(i, 1);
			gameDeck.Add (newCard);
		}
	}
	public void GenHearts() {
		// Suit identifier of 2 for hearts
		for (int i = 1; i < 14; i++) {
			Card newCard = new Card(i, 2);
			gameDeck.Add (newCard);
		}
	}
	public void GenSpades() {
		// Suit identifier of 3 for spades
		for (int i = 1; i < 14; i++) {
			Card newCard = new Card(i, 3);
			gameDeck.Add (newCard);
		}
	}
	public void GenClubs() {
		// Suit identifier of 4 for clubs
		for (int i = 1; i < 14; i++) {
			Card newCard = new Card(i, 4);
			gameDeck.Add (newCard);
		}
	}

	// Shuffle the deck using the Fisher-Yates Shuffle algorithm
	public void ShuffleDeck() {
		for (int i = gameDeck.Count - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			Card temp = gameDeck [i];
			gameDeck [i] = gameDeck [r];
			gameDeck [r] = temp;
		}
	}
		
	// Seperate the shuffled deck into two queues to represent each player
	public void SplitDeck() {
		int i = 0;
		foreach (Card c in gameDeck) {
			// Add to player on evens, opponent on odds
			if (i % 2 == 0) {
				player.Enqueue (c);
			} else {
				computer.Enqueue (c);
			}
			i++;
		}
	}


	// Run through one card flip (one turn)
	public void PlayCard() {
		Card pCard = player.Dequeue ();
		Card cCard = computer.Dequeue ();

		prizePool.Enqueue (pCard);
		prizePool.Enqueue (cCard);

		if (pCard.GetValue () == cCard.GetValue ()) {
			AddToPool();
			PlayCard ();
		} else if (pCard.GetValue () > cCard.GetValue ()) {
			Debug.Log ("player win");
			foreach (Card p in prizePool) {
				player.Enqueue (p);
			}
		} else {
			Debug.Log ("computer win");
			foreach (Card p in prizePool) {
				computer.Enqueue (p);
			}
		}

		// Clear out prizePool for next round
		prizePool.Clear ();

		Debug.Log ("playercount: " + player.Count);
		Debug.Log ("computercount: " + computer.Count);
	}

	// Tiebreaker function: Rules from Bicycle Cards website:
	// If the cards are the same rank, it is War.  Each player turns up one card
	// face down and one card face up. The player with the higher cards takes both
	// piles (six cards).  If the turned-up cards are again the same rank, each
	// player places another card face down and turns another card face up.  The
	// player with the higher card takes all 10 cards, and so on.
	public void AddToPool() {
		// Add the "face down" cards to the prize pool
		prizePool.Enqueue (player.Dequeue ());
		prizePool.Enqueue (computer.Dequeue ());
	}
}
