/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package GeometryDataStructure;

/**
 *
 */
public class Point {

   /* Data Members */
   public double X,Y,Z;

   public Point(double x,double y,double z)
   {
       this.X = x;
       this.Y = y;
       this.Z = z;
   }
   public boolean  Equals(Point p)
   {
      return (this.X == p.X && this.Y == p.Y && this.Z == p.Z);
   }
   public static double GetDistance(Point p1, Point p2)
   {
       return Math.sqrt((p1.X-p2.X)*(p1.X-p2.X) + (p1.Y-p2.Y)*(p1.Y-p2.Y) + (p1.Z-p2.Z)*(p1.Z-p2.Z));
   }
   //Function to Check if Line p0p1 makes left turn to p1p2.
   public static boolean LeftTurn(Point p0,Point p1,Point p2)
   {
     Vector a = new Vector();
     Vector b = new Vector();

     a.Set(p1, p0);
     b.Set(p2, p0);

     if(Vector.ToRight(a, b) || Collinear(p0,p1,p2))
         return true;
     else
         return false;
   }
   public static boolean Collinear(Point p0,Point p1,Point p2)
   {
       Vector a=new Vector();
       Vector b = new Vector();

       a.Set(p1,p0);
       b.Set(p2,p0);

       return (Vector.CrossProduct(a, b).Z == 0);
   }

}
