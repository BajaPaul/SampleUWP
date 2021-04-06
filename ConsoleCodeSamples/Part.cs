using System;

/* 
 * Found sample at: https://msdn.microsoft.com/en-us/library/b0zbh7b6(v=vs.110).aspx
 * 
 * More about IEquatable<T> Interface at:
 * https://msdn.microsoft.com/en-us/library/ms131187(v=vs.110).aspx
 * https://msdn.microsoft.com/en-us/library/ms131190(v=vs.110).aspx
 * 
 * More about IComparable<T> Interface at:
 * https://msdn.microsoft.com/en-us/library/4d7sx9hd(v=vs.110).aspx
 */

namespace SampleUWP.ConsoleCodeSamples
{
    // This class is used in Sample02_List_Methods.  Simple business object.  
    // A PartId is used to identify a part but the part name can change. 
    internal class Part            // : IEquatable<Part>, IComparable<Part>       // Uses two generic interfaces!
    {
        public string PartName { get; set; }

        public int PartId { get; set; }

        public override string ToString()
        {
            return "ID: " + PartId + "   Name: " + PartName;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    Part objAsPart = obj as Part;
        //    if (objAsPart == null) return false;
        //    else return Equals(objAsPart);
        //}

        //public int SortByNameAscending(string name1, string name2)
        //{
        //    return name1.CompareTo(name2);
        //}

        //// Default comparer for Part type.
        //public int CompareTo(Part comparePart)
        //{
        //    // A null value means that this object is greater.
        //    if (comparePart == null)
        //        return 1;
        //    else
        //        return this.PartId.CompareTo(comparePart.PartId);
        //}

        //public override int GetHashCode()
        //{
        //    return PartId;
        ////}

        //public bool Equals(Part other)
        //{
        //    if (other == null) return false;
        //    return (this.PartId.Equals(other.PartId));
        //}
        // Should also override == and != operators.
    }
}
