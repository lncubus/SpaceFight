using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SF.Space
{
    [System.Serializable]
    public class QuadBez
    {
        public Vector3 st, en, ctrl;

        public QuadBez(Vector3 st, Vector3 en, Vector3 ctrl)
        {
            this.st = st;
            this.en = en;
            this.ctrl = ctrl;
        }


        public Vector3 Interp(double t)
        {
            double d = 1f - t;
            return st * (d * d) + ctrl * (2f * d * t) + en * (t * t);
        }


        public Vector3 Velocity(double t)
        {
            return (st * 2f - ctrl * 4f + en * 2f) * t + ctrl * 2f - st * 2f;
        }
    }


    [System.Serializable]
    public class CubicBez
    {
        public Vector3 st, en, ctrl1, ctrl2;

        public CubicBez(Vector3 st, Vector3 en, Vector3 ctrl1, Vector3 ctrl2)
        {
            this.st = st;
            this.en = en;
            this.ctrl1 = ctrl1;
            this.ctrl2 = ctrl2;
        }


        public Vector3 Interp(double t)
        {
            double d = 1f - t;
            return st * (d * d * d) + ctrl1 * (3f * d * d * t) + ctrl2 * (3f * d * t * t) + en * (t * t * t);
        }


        public Vector3 Velocity(double t)
        {
            return (st * -3f + ctrl1 * 9f - ctrl2 * 9f + en * 3f) * t * t
                + (st * 6f - ctrl1 * 12f + ctrl2 * 6f) * t
                - st * 3f + ctrl1 * 3f;
        }
    }


    [System.Serializable]
    public class CRSpline
    {
        public Vector3[] pts;

        public CRSpline(params Vector3[] pts)
        {
            this.pts = new Vector3[pts.Length];
            System.Array.Copy(pts, this.pts, pts.Length);
        }


        public Vector3 Interp(double t)
        {
            int numSections = pts.Length - 3;
            int currPt = System.Math.Min((int)(System.Math.Floor(t * (double)numSections)), numSections - 1);
            double u = t * (double)numSections - (double)currPt;

            Vector3 a = pts[currPt];
            Vector3 b = pts[currPt + 1];
            Vector3 c = pts[currPt + 2];
            Vector3 d = pts[currPt + 3];

            return (
                (a * -1f + b * 3f - c * 3f + d) * (u * u * u)
                + (a * 2f - b * 5f + c * 4f - d) * (u * u)
                + (c - a) * u
                + b * 2f
            ) * .5f;
        }


        public Vector3 Velocity(double t)
        {
            int numSections = pts.Length - 3;
            int currPt = System.Math.Min((int)System.Math.Floor(t * (double)numSections), numSections - 1);
            double u = t * (double)numSections - (double)currPt;

            Vector3 a = pts[currPt];
            Vector3 b = pts[currPt + 1];
            Vector3 c = pts[currPt + 2];
            Vector3 d = pts[currPt + 3];

            return (b * 3f - a - c * 3f + d) * 1.5f *(u * u)
                    + (a * 2f - b * 5f + c * 4f - d) * u
                    + c * .5f - a *.5f;
        }

        public IEnumerable<Vector> Interpolate(int numSegments)
        {
            for (int i = 0; i <= numSegments; ++i)
            {
                var ret = Interp((double)i / (double)numSegments);
                yield return new Vector(ret.x, ret.y);
            }
        }

        public static CRSpline Create(List<Vector> points)
        {
            List<Vector3> splinePoints = new List<Vector3>();
            splinePoints.Add(new Vector3(points[0])); // invisible start
            splinePoints.AddRange(points.Select(x => new Vector3(x)));
            splinePoints.Add(splinePoints[splinePoints.Count - 1]); // invisible end
            return new CRSpline(splinePoints.ToArray());
        }
    }
}