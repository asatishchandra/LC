using System;
using System.Collections.Generic;
using System.Text;

namespace LC
{
	public class PriorityQueue<T> where T : IComparable<T>
	{
		//https://gist.github.com/trevordixon/10401462
		//any parent node in the list at index [pi], the two child nodes are located at indexes [2 * pi + 1] and [2 * pi + 2].
		//For a given child node at index [ci], its parent is located at index [(ci - 1) / 2].

		//This is a Min Heap
		//To make it a Max Heap refer comments
		private List<T> data;

		public PriorityQueue()
		{
			this.data = new List<T>();
		}

		public void Enqueue(T item)
		{
			data.Add(item);
			int ci = data.Count - 1;
			while (ci > 0)
			{
				int pi = (ci - 1) / 2;
				if (data[ci].CompareTo(data[pi]) >= 0) //This is MIN Heap
				//if(data[ci].CompareTo(data[pi]) <= 0) //This is MAX Heap
					break;
				Swap(pi, ci);
				ci = pi;
			}
		}

		public T Dequeue()
		{
			int li = data.Count - 1;
			T frontItem = data[0];
			data[0] = data[li];
			data.RemoveAt(li);
			--li; //last index after removal

			int pi = 0;
			while (true)
			{
				int ci = 2 * pi + 1; //left child index of parent
				if (ci > li)
					break;  //no children so done
				int rc = ci + 1; //right child
				if (rc <= li && (data[rc].CompareTo(data[ci]) < 0)) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead  -- FOR MIN HEAP
																	//if(rc <= li && (data[rc].CompareTo(data[ci]) > 0)) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead  -- FOR MAX HEAP
					ci = rc;

				if (data[pi].CompareTo(data[ci]) <= 0) // FOR MIN HEAP
													   //if (data[pi].CompareTo(data[ci]) >= 0) // FOR MAX HEAP
					break; // parent is smaller than (or equal to) smallest child so done
				Swap(pi, ci);
				pi = ci;
			}
			return frontItem;
		}

		public void Swap(int pi, int ci)
		{
			T temp = data[pi];
			data[pi] = data[ci];
			data[ci] = temp;
		}

		public int Count()
		{
			return data.Count;
		}

		public T Peek()
		{
			return data[0];
		}

		public void Print()
		{
			Console.WriteLine("Printing the current PriorityQueue items...");
			Console.WriteLine("Total elements: " + data.Count);
			foreach (var item in data)
			{
				Console.WriteLine(item.ToString());
			}
		}
	}
}
