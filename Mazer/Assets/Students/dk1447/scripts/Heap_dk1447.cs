using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap_dk1447<T> where T : IHeapItem<T>{ //generic class with constraint, this is still a fuzzy concept for me

	T[] items; //generic type array
	int currentItemCount;

	public Heap_dk1447(int maxHeapSize){ //constructor for heap
		items = new T[maxHeapSize]; //set items to an array of the desired maximum size
	}

	public void Add(T item){ //method for adding an item to the array
		item.HeapIndex = currentItemCount; //set the heap index of the item to the current count
		items[currentItemCount] = item; //in the items array, set the index at the current count to the item
		SortUp(item); //call our sort method on the item
		currentItemCount++; //increase the item count
	}


	public T RemoveFirst(){ //removes the first item in the array
		T firstItem = items[0]; //get the first item in our array
		currentItemCount--; //decrement item count
		items[0] = items[currentItemCount]; //moves the last item to the first spot in the array
		items[0].HeapIndex = 0; //updates the item with its new index
		SortDown(items[0]);
		return firstItem;
	}

	public void UpdateItem (T item){ //used to update an item's priority
		SortUp(item);
	}

	public int Count{ //get the number of items in the heap
		get{
			return currentItemCount;
		}
	}

	public bool Contains(T item){ //checks if there is a specific item in the heap
		return Equals(items[item.HeapIndex], item); //check if there is an item with the same index as the one being passed in
	}

	void SortDown(T item){
		while(true){
			int childIndexLeft = item.HeapIndex * 2 + 1; //find the left child of the item
			int childIndexRight = item.HeapIndex * 2 + 2; //find the right child of the item

			int swapIndex = 0; //the index of the item we want to swap with

			if (childIndexLeft < currentItemCount){//if the item has a left child
				swapIndex = childIndexLeft; //set the swap index to that child
				if (childIndexRight < currentItemCount){ //if the item also has a right child
					if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0){ //if the left child has a lower priority than the right
						swapIndex = childIndexRight; //set the swap index to the right child
					}
				}

				if (item.CompareTo(items[swapIndex]) < 0){  //if the parent item has a lower priority than the highest priority child
					Swap(item,items[swapIndex]); //swap the parent with the child
				} 
				else{
					return; //if the parent has a higher priority than it's children, it's in the right place so do nothing
					}
			}
			else{
				return; //if the parent has no children, it's actually not a parent! so it can't be sorted down, it's in the right place, do nothing
			}
		}
	}

	void SortUp(T item){
		int parentIndex = (item.HeapIndex - 1)/2; //find the index of the item's parent

		while(true){
			T parentItem = items[parentIndex]; //gets the item at the parent index
			if (item.CompareTo(parentItem) > 0){ //if the item has a higher priority than the parent item
				Swap (item, parentItem); //swap them
			}
			else{
				break;
			}

			parentIndex = (item.HeapIndex - 1)/2; //
		}
	}

	void Swap(T itemA, T itemB){ //swaps two items
		items[itemA.HeapIndex] = itemB; //swap the two items into eachother's indexes
		items[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex; //update the items with their new indexes, using a temp variable
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}

}

public interface IHeapItem<T> : IComparable<T> {// I sort of get this? I know that IComparable is what alllows for comparison and sorting
	int HeapIndex{ //where the item is in the heap
		get;
		set;
	}
}