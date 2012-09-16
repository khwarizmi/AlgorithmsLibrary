/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package GeometryDataStructure;

/**
  * class Vector is responsible for simple Vector Operations.
  */
public class Vector {

  /* data members */
  public double X,Y,Z;

  public Vector()
  {}
  public Vector(double x,double y,double z)
  {
    this.X = x;
    this.Y = y;
    this.Z = z;
  }

  //Vector norm
  public double norm()
  {
    return Math.sqrt(X*X + Y*Y +Z*Z);
  }

  public void Set(Point p1,Point p2)
  {
    X = p1.X - p2.X;
    Y = p1.Y - p2.Y;
    Z = p1.Z - p2.Z;
  }

  /* Operator Overloading */

  //TODO : Implement Operator Overloading Operators.


  /* Static Functions */

  //Add two Vectors
  static public Vector Add(Vector a,Vector b)
  {
    return new Vector(a.X + b.X , a.Y + b.Y, a.Z + b.Z);
  }

  //Dot Product two Vectors
  static public double DotProduct(Vector a,Vector b)
  {
     return ( a.X*b.X + a.Y+b.Y + a.Z*b.Z);
  }

  // A ⋅ B = |A||B|Cos(θ)
  static public double AngleBetween(Vector a,Vector b)
  {
     return Math.acos( DotProduct(a,b)/( a.norm() * b.norm() ));
  }

  //Cross Product two Vectors
  static public Vector CrossProduct(Vector a , Vector b)
  {
     return new Vector( (a.Y * b.Z) - (b.Y * a.Z)
                       ,(a.Z * b.X) - (b.Z * a.X)
                       ,(a.X * b.Y) - (b.X * a.Y));
  }

  //In 2-D geometry means that if A is less than 180 degrees clockwise from B, the Theta value is positive.
  static public Boolean ToRight(Vector a , Vector b)
  {
     return ( CrossProduct(a,b).Z > 0 );
  }

  //In 2-D geometry to get angle of vector
  public double Angle()
  {
      double angle = Math.toDegrees(Math.atan2(Y,X));

      if(angle<0)
          angle+=360;
      
      return angle;
  
      
  }

}
