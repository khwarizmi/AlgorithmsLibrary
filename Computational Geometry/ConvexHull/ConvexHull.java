/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
 
package ConvexHull;

import GeometryDataStructure.*;
import java.util.ArrayList;
import java.util.Queue;
import java.util.Stack;
//import java.util.Collection;

/**
 * class to Implement Convex Hull Algorithms in 2D-Space.
 */
public class ConvexHull {

public ArrayList<Line>  ConvexHull_BruteForce(ArrayList<Point> points)
{
    boolean convexLine = false;
    Vector a = new Vector();
    Vector b = new Vector();
    
    //arraylist of type 'Line' holding alllines of Convex hull
    ArrayList<Line> ConvexLines = new ArrayList<Line>();

    /* Brute Force : iterate over all combination in Set of Points and
     * check if all points are to the right of Line ij (clock-wise picking).
     */
  for(int i=0; i<points.size() ; i++)
     for(int j=0; j<points.size() ;j++)
         if( i != j)
         {
             convexLine = true;
             a.Set(points.get(j), points.get(i));

          for(int k=0; k<points.size() ;k++)
           if( k!= i && k!= j)
           {
               b.Set(points.get(k), points.get(i));
               
              if(Vector.ToRight(a,b))
              {
                 convexLine = false;
                 break;
              }
           }

          if(convexLine)
           ConvexLines.add(new Line(points.get(i),points.get(j)));
         }

  return ConvexLines;
}
public Stack<Point>  ConvexHull_GrahamScan(ArrayList<Point> points)
{

    if(points.size()<=0)
        return new Stack<Point>();

   ArrayList<Point> InputPoints = new ArrayList<Point>(points);
   Point  p0 = InputPoints.get(0);
   // stack to hold points in order they are added to convex hull descending.
   Stack<Point> sortedpoints = new Stack<Point>();
   
   /* choose point P0 with smallest Y value in case of tie choose mostleft */
   for(int i=0;i<points.size();i++)
      if(p0.Y > points.get(i).Y || (p0.Y == points.get(i).Y && p0.X > points.get(i).X))
          p0 = points.get(i);

   
   InputPoints.remove(p0);

   /* Sort points according to Polar angle */
   java.util.Collections.sort(InputPoints, new GrahamPointSorting(p0));

   // add first three points.
   sortedpoints.push(p0);
   sortedpoints.push(InputPoints.get(0));
   sortedpoints.push(InputPoints.get(1));
   
   for(int i=2;i<InputPoints.size();i++)
   {
       while(sortedpoints.size() > 2)
       {
         if(!Point.LeftTurn(sortedpoints.get(sortedpoints.size()-2), sortedpoints.peek(), InputPoints.get(i)))
             sortedpoints.pop();
         else
             break;
       }

       sortedpoints.push(InputPoints.get(i));
   }


   return sortedpoints;
}
public ArrayList<Point> ConvexHull_JarvisMarch(ArrayList<Point> points)
{

    int crnt,next=-1,root=-1;
    Vector a = new Vector();
    Vector b = new Vector();
    //boolean[] vpoints = new boolean[points.size()];

    //arraylist of type 'Line' holding alllines of Convex hull
    ArrayList<Point> ConvexPoints = new ArrayList<Point>();

    if(points.size()<= 0)
        return ConvexPoints;

    //choose point with lowest X-value.
    crnt = 0;
    for(int i=0;i<points.size();i++)
        if(points.get(i).X<points.get(crnt).X || (points.get(i).X==points.get(crnt).X && points.get(i).Y<points.get(crnt).Y))
            crnt = i;

    //add first point in convex hull.
    ConvexPoints.add(points.get(crnt));
    root = crnt;

    while(true)
    {
       next = -1;
       for(int i=0;i<points.size();i++)
       {
         if(i == crnt)
            continue;

         if(next == -1)
         {
             next = i;
             a.Set(points.get(next), points.get(crnt));
             continue;
         }
         
         
         b.Set(points.get(i),points.get(crnt));

         if(Vector.ToRight(a, b))
         {
           next = i;
           a.Set(points.get(next), points.get(crnt));
         }
       }

       //break condition when no next point to go to.
       if(next == -1 || next == root)
          break;
      
       //vpoints[next] = true;
       ConvexPoints.add(points.get(next));
       crnt = next;
    }


    return ConvexPoints;
}
}


 class GrahamPointSorting implements java.util.Comparator<Point> {


  Point p0 ;
  Vector a,b;

  
  public GrahamPointSorting(Point P0)
  {
      p0 = P0;
      a = new Vector();
      b = new Vector();
  }
  public int compare(Point p1, Point p2) {

     a.Set(p1, p0);
     b.Set(p2, p0);

    if(a.Angle() > b.Angle())
        return 1;
    else
        if(a.Angle() < b.Angle())
            return -1;

    if(a.norm() > b.norm())
       return 1;
    else
       return -1;
      
 }
}
