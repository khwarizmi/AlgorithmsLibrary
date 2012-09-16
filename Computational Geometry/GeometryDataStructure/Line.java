/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package GeometryDataStructure;

/**
 *
 * @author feras
 */
public class Line {

  Point p1;
  Point p2;
public Line(Point point1,Point point2)
{
    p1 = point1;
    p2 = point2;
}

public boolean  PointOnLine(Point p)
{
   return ( Math.min(p1.X,p2.X) <= p.X && p.X <= Math.max(p1.X,p2.X) )
          &&
          ( Math.min(p1.Y,p2.Y) <= p.Y && p.Y <= Math.max(p1.Y,p2.Y) );
}


/*Get Point of Intersection of Line */
Point LineIntersect( Line l1 , Line l2 , boolean  isLineSegment)
{
  /*
   * Line Equation : Ax + By = C  => According to Slope Equation.
   * A = Y2 - Y1 , B = X1 - X2 , C = (A * X1) + (B * Y1).
   */
/*
  // First Line Constants.
  double A1 = l1.Y2 - l1.Y1;
  double B1 = l1.X1 - l1.X2;
  double C1 = (A1 * l1.X1) + (B1 * l1.Y1);

  // Second Line Constants.
  double A2 = l2.Y2 - l2.Y1;
  double B2 = l2.X1 - l2.X2;
  double C2 = (A2 * l2.X1) + (B2 * l2.Y1);


  double det = (A1 * B2) - (A2 * B1);

  if( det == 0 )
   {
      // lines are parallel
      return null;
   }
   else
       {
          double x = (B2*C1 - B1*C2)/det;
          double y = (A1*C2 - A2*C1)/det;
          Point interPoint = new Point(x,y);

          // Condition : if Line Segments && Not on the First Line or The Second Line return  null
          if(isLineSegment)
          if( !l1.PointOnLine(interPoint) || !l2.PointOnLine(interPoint)
            return null;


          return interPoint;
       }
*/
   return null;
}

public Point getPoint1()
{
  return p1;
}

public Point getPoint2()
{
  return p2;
}

}
