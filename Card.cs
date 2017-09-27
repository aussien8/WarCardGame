using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

	// Information for the card
	private int value;
	// 1 - Diamonds, 2 - Hearts, 3 - Spades, 4 - Clubs
	private int suit;


	// Information for card graphics
	private bool faceUp;


	// Constructor
	public Card(int newValue, int newSuit) {
		value = newValue;
		suit = newSuit;
		faceUp = false;
	}

	// Get functions for card values
	public int GetValue() {
		return value;
	}
	public int GetSuit() {
		return suit;
	}
	public bool GetFace() {
		return faceUp;
	}

	// Set function for card face direction
	public void SetFace(bool newFace) {
		faceUp = newFace;
	}
		

	public void PrintCard() {
		Debug.Log ("value: " + value + "   suit: " + suit);
	}
		
}
