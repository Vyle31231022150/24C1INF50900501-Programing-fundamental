using System;
namespace ConsoleApp1 
{
  internal class Session06 
  {
    ///average
    static double CalculateAverageValue(int[]a)
    {
      double sum = 0d;
      foreach(int i in a)
      {
        sum += i;
      }
      return sum / a.Length;
    }
    ///check if array contains a specific value
    static bool CheckExist(int[] a, int value)
    {
      foreach(int val in a)
         if (val != value) return false;
            return true;
    }
    ///remove element
    static bool RemoveElement(int[] a, int ele)
    {
      for (int i = 0; i < a.Length;i++)
        {
          if (a[i] == ele)
          {
            for (int j = i; j < a.Length -1 ; j++)
            {
              a[j] = a[j+1];
            }
            return true;//loai bo phan tu dau tien tim thay
          }
        }
      return false;//khong co phan tu can bo
    }
    ///Find the index of an array element
    static int FindIndexOfAnArray(int[] a, int value)
    {
      for (int i = 0; i <= a.Length; i++)
      {
        foreach(int val in a)
        {
          if (val == a[i]) return i;
        }
      }
      return -1;
    }
    ///reverse an array
    static int [] ReverseArray(int[] a)
    {
      for (int i = a.Length - 1; i >=0; i--)
      {
          Console.Write($"{a[i]}");
      }
      return a;
    }
    ///Find max and min of an Array
    static void MaxAndMinElementArray (int []a)
    {
      int max = a[0];
      int  min = a[0];
      foreach (int val in a)
      {
        if(min < val) min = val;
        if (max > val) max = val;
      }
      Console.WriteLine($"Min: {min}");
      Console.WriteLine( $"Max: {max}");
    }
    ///find duplicate elements from an array
    static void FindDuplicateElements(int[] c)
    {
      for (int i = 0; i < c.Length; i++)
       if( c[i] == c[i+1]) Console.WriteLine($"{c[i]} is a duplicate value in the array");
    }
    ///Remove duplicate elements
    static bool RemoveDuplicateElements(int[] c)
    {
      for (int i = 0; i < c.Length; i++)
      {
        if (c[i] == c[i+1])
        {
          for (int j = i; j < c.Length -1 ; j++)
                {
                  c[j] = c[j+1];
                }
                return true;//loai bo phan tu dau tien tim thay
        }
      }
          return false;//khong co phan tu can bo
    }
    ///Bubble sort
    static void BubbleSort(int[] a)
    {
      int [] nums = new int[10];
      for (int i = 0; i < nums.Length; i++)
      {
        nums[i] = Convert.ToInt32(Console.ReadLine());
      }
      for (int i = 0; i < nums.Length; i++)
      {
        for (int j = i + 1; j < nums.Length; j++)
        {
          if (nums[i] > nums[j])
          {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
          }
        }
      }
      foreach (int val in nums)
      {
        Console.Write($"{val} ");
      }
    }
    ///if the word appears in the phrase
    static bool CheckWordExistInSentence(string sentence, string word)
    {
      char [] s = sentence.ToCharArray();
      char [] w = word.ToCharArray();
      ///chuyen ve dang kiem tra mot mang co trong mot mang hay khong
    }
    public static void Mainx (string[] args) 
    {
      int [] a = new int[5] {1,5,6,9,10};//khai bao + khoi tao doi tuong
      bool exsit = CheckExist(a,6);
      int [] b = new int[5] {3,5,6,9,10};
        int [] c = new int[5] {3,5,5,6,9};
        bool r2 = RemoveElement(b,6);
        if (r2 == true) Array.Resize(ref b, b.Length - 1);
      int index = FindIndexOfAnArray(a, 1);
        Console.WriteLine($"Index of 1: {index}");
        ReverseArray(a);
        FindDuplicateElements(c);
        bool r3 = RemoveDuplicateElements(c);
          if (r3 == true) Array.Resize(ref c, c.Length - 1);
    }
   
  }
}
    
