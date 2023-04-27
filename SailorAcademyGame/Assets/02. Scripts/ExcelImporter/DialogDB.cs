using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DialogDB : ScriptableObject
{
	public List<DialogDBCharacter> character;
	public List<DialogDBChapter> prolog; // Replace 'EntityType' to an actual type that is serializable.
	
}
