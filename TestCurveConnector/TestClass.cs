using System;
using System.Collections.Generic;
using System.Linq;
using CTS_Library;
using FastClass;
using NUnit.Framework;
using Snap;

namespace TestCurveConnector
{
    [TestFixture]
    public class TestClass
    {
        [SetUp]
        public void SetUp()
        {
            Globals.DisplayPart = Snap.NX.Part.OpenPart("P:\\CurveConnectorTest.prt");
        }

        [TearDown]
        public void TearDown()
        {
            Globals.DisplayPart.Close(true, true);
        }

        /// <summary>
        /// Tests to make sure, that when given the startpoint of a curve, the endpoint of that curve is returned and vice versa.
        /// </summary>
        [Test]
        public void Test_OtherPosition_Valid()
        {
            var position1 = new Position(1, 1, 1);
            var position2 = new Position(2, 2, 2);
            var curve = Create.Line(position1, position2);
            var other = curve.GetOtherPosition(position1);
            Assert.IsTrue(other.IsEqualTo(position2));
            other = curve.GetOtherPosition(position2);
            Assert.IsTrue(other.IsEqualTo(position1));
            Snap.NX.NXObject.Delete(curve);
        }

        /// <summary>
        /// Tests to make sure, that when given a curve and position that is not equal to either startpoint or endpoint of that curve, that an exception of type "ArgumentOutOfRangeException" is thrown when "GetOtherPosition" is asked.
        /// </summary>
        [Test]
        public void Test_OtherPosition_Invalid()
        {
            var position1 = new Position(1, 1, 1);
            var position2 = new Position(2, 2, 2);
            var position3 = new Position(3, 3, 3);
            var curve = Create.Line(position1, position2);
            Assert.Throws<ArgumentOutOfRangeException>(() => curve.GetOtherPosition(position3));
            Snap.NX.NXObject.Delete(curve);
        }

        [Test]
        public void Test_Contains_True()
        {
            var curvesLayer2 = Globals.DisplayPart.Curves.Where(curve => curve.Layer == 2).ToArray();
            var connector = CurveConnector.Create(curvesLayer2);
            foreach (var curve in curvesLayer2)
                Assert.IsTrue(connector.Contains(curve));
        }

        [Test]
        public void Test_Contains_False()
        {
            var curvesLayer2 = Globals.DisplayPart.Curves.Where(curve => curve.Layer == 2).ToArray();
            var connector = CurveConnector.Create(curvesLayer2);
            var position1 = new Position(1, 1, 1);
            var position2 = new Position(2, 2, 2);
            var tempCurve = Create.Line(position1, position2);
            Assert.IsFalse(connector.Contains(tempCurve));
            Snap.NX.NXObject.Delete(tempCurve);
        }

        [Test]
        public void Test_AreConnected_True()
        {
            var position1 = new Position(1, 1, 1);
            var position2 = new Position(2, 2, 2);
            var position3 = new Position(3, 3, 3);
            var curve1 = Create.Line(position1, position2);
            var curve2 = Create.Line(position1, position3);
            Assert.IsTrue(curve1.AreConnected(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_AreConnected_False()
        {
            var position1 = new Position(1, 1, 1);
            var position2 = new Position(2, 2, 2);
            var position3 = new Position(3, 3, 3);
            var position4 = new Position(4, 4, 4);
            var curve1 = Create.Line(position1, position2);
            var curve2 = Create.Line(position3, position4);
            Assert.IsFalse(curve1.AreConnected(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_AreConnectedByStartPoint_True()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(startPoint, endPoint2);
            Assert.IsTrue(curve1.IsConnected(curve2));
            Assert.IsTrue(curve1.IsConnectedByStartPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_AreConnectedByStartPoint_False()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var otherStartPoint = endPoint1;
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(otherStartPoint, endPoint2);
            Assert.IsTrue(curve1.IsConnected(curve2));
            Assert.IsFalse(curve1.IsConnectedByStartPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_AreConnectedByEndPoint_True()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint);
            var curve2 = Create.Line(endPoint2, endPoint);
            Assert.IsTrue(curve1.IsConnected(curve2));
            Assert.IsTrue(curve1.IsConnectedByEndPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_AreConnectedByEndPoint_False()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(startPoint, endPoint2);
            Assert.IsTrue(curve1.IsConnected(curve2));
            Assert.IsFalse(curve1.IsConnectedByEndPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_IsCurveConnectedByStartPoint()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(startPoint, endPoint2);
            var connector = CurveConnector.Create();
            Assert.IsTrue(connector.IsConnectedByStartPoint(curve1));
            Assert.IsTrue(connector.IsConnectedByStartPoint(curve2));
            Assert.IsFalse(connector.IsConnectedByEndPoint(curve1));
            Assert.IsFalse(connector.IsConnectedByEndPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_IsCurveConnectedByEndPoint()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint);
            var curve2 = Create.Line(endPoint2, endPoint);
            var connector = CurveConnector.Create();
            Assert.IsFalse(connector.IsConnectedByStartPoint(curve1));
            Assert.IsFalse(connector.IsConnectedByStartPoint(curve2));
            Assert.IsTrue(connector.IsConnectedByEndPoint(curve1));
            Assert.IsTrue(connector.IsConnectedByEndPoint(curve2));
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_GetCurvesConnectedToPoint_Valid_NoIgnore()
        {
            var position = new Position(-108.390000000, -121.070000000, 0.000);
            var connector = CurveConnector.Create();
            var curves = connector.GetConnectedCurves(position).ToArray();
            Assert.AreEqual(2, curves.Length);
            Assert.IsTrue(curves.SingleOrDefault(curve => curve.Name == "TEST_1") != null);
            Assert.IsTrue(curves.SingleOrDefault(curve => curve.Name == "TEST_2") != null);
        }

        [Test]
        public void Test_GetCurvesConnectedToPoint_Valid_WithIgnore()
        {
            var position = new Position(-108.390000000, -121.070000000, 0.000);
            var connector = CurveConnector.Create();
            var curveToIgnore = (Snap.NX.Curve) Snap.NX.NXObject.FindByName("TEST_1");
            var curves = connector.GetConnectedCurves(position, curveToIgnore).ToArray();
            Assert.AreEqual(1, curves.Length);
            Assert.IsTrue(curves.SingleOrDefault(curve => curve.Name == "TEST_1") == null);
            Assert.IsTrue(curves.SingleOrDefault(curve => curve.Name == "TEST_2") != null);
        }

        [Test]
        public void Test_GetCurvesConnectedToPoint_Invalid()
        {
            var positionNotAttached = new Position(-1.0, -121.070000000, 0.000);
            var connector = CurveConnector.Create();
            Assert.Throws<KeyNotFoundException>(() => connector.GetConnectedCurves(positionNotAttached));
        }

        [Test]
        public void Test_GetCurvesConnectedToStartPoint_Valid()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(startPoint, endPoint2);
            var connector = CurveConnector.Create();
            var connectedCurves = connector.GetConnectedCurvesToStartPoint(curve1);
            Assert.AreEqual(curve2.NXOpenTag, connectedCurves.Single().NXOpenTag);
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_GetCurvesConnectedToEndPoint_Invalid()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint1 = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint1);
            var curve2 = Create.Line(startPoint, endPoint2);
            var connector = CurveConnector.Create();
            var connectedCurves = connector.GetConnectedCurvesToEndPoint(curve1);
            CollectionAssert.IsEmpty(connectedCurves);
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_GetCurvesConnectedToStartPoint_Invalid()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint);
            var curve2 = Create.Line(endPoint2, endPoint);
            var connector = CurveConnector.Create();
            var connectedCurves = connector.GetConnectedCurvesToStartPoint(curve1);
            CollectionAssert.IsEmpty(connectedCurves);
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_GetCurvesConnectedToEndPoint_Valid()
        {
            var startPoint = new Position(1, 1, 1);
            var endPoint = new Position(2, 2, 2);
            var endPoint2 = new Position(3, 3, 3);
            var curve1 = Create.Line(startPoint, endPoint);
            var curve2 = Create.Line(endPoint2, endPoint);
            var connector = CurveConnector.Create();
            var connectedCurves = connector.GetConnectedCurvesToEndPoint(curve1);
            Assert.AreEqual(curve2.NXOpenTag, connectedCurves.Single().NXOpenTag);
            Snap.NX.NXObject.Delete(curve1, curve2);
        }

        [Test]
        public void Test_GetChainedCurvesFromEndPoint_Valid()
        {
            var point1 = new Position(1, 1, 1);
            var point2 = new Position(2, 2, 2);
            var point3 = new Position(3, 3, 3);
            var point4 = new Position(4, 4, 4);
            var chainedCurves = new List<Snap.NX.Curve> {Create.Line(point1, point2), Create.Line(point3, point2), Create.Line(point4, point3)};
            var connector = CurveConnector.Create();
            var expectedTags = chainedCurves.Select(curve => curve.NXOpenTag).ToArray();
            var actualTags = connector.GetChainedCurvesFromEndPoint(chainedCurves.First()).Select(curve => curve.NXOpenTag).ToArray();
            CollectionAssert.AreEqual(expectedTags, actualTags);
            Snap.NX.NXObject.Delete(chainedCurves.ToArray<Snap.NX.NXObject>());
        }

        [Test]
        public void Test_GetChainedCurvesFromEndPoint_Invalid()
        {
            var point1 = new Position(1, 1, 1);
            var point2 = new Position(2, 2, 2);
            var point3 = new Position(3, 3, 3);
            var point4 = new Position(4, 4, 4);
            var chainedCurves = new List<Snap.NX.Curve> {Create.Line(point2, point1), Create.Line(point3, point2), Create.Line(point4, point3)};
            var connector = CurveConnector.Create();
            var result = connector.GetChainedCurvesFromEndPoint(chainedCurves.First());
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void Test_GetChainedCurvesFromStartPoint_Valid()
        {
            var point1 = new Position(1, 1, 1);
            var point2 = new Position(2, 2, 2);
            var point3 = new Position(3, 3, 3);
            var point4 = new Position(4, 4, 4);
            var chainedCurves = new List<Snap.NX.Curve> {Create.Line(point2, point1), Create.Line(point3, point2), Create.Line(point4, point3)};
            var connector = CurveConnector.Create();
            var expectedTags = chainedCurves.Select(curve => curve.NXOpenTag).ToArray();
            var actualTags = connector.GetChainedCurvesFromStartPoint(chainedCurves.First()).Select(curve => curve.NXOpenTag).ToArray();
            CollectionAssert.AreEqual(expectedTags, actualTags);
            Snap.NX.NXObject.Delete(chainedCurves.ToArray<Snap.NX.NXObject>());
        }

        [Test]
        public void Test_GetChainedCurvesFromStartPoint_Invalid()
        {
            var point1 = new Position(1, 1, 1);
            var point2 = new Position(2, 2, 2);
            var point3 = new Position(3, 3, 3);
            var point4 = new Position(4, 4, 4);
            var chainedCurves = new List<Snap.NX.Curve> {Create.Line(point1, point2), Create.Line(point3, point2), Create.Line(point4, point3)};
            var connector = CurveConnector.Create();
            var result = connector.GetChainedCurvesFromStartPoint(chainedCurves.First());
            CollectionAssert.IsEmpty(result);
            Snap.NX.NXObject.Delete(chainedCurves.ToArray<Snap.NX.NXObject>());
        }
        
        [Test]
        public void Test_GetPolylineFromStartPoint_Valid()
        {
            var bottomLeft = new Position(100, 100);
            var bottomRight = new Position(200, 100);
            var topLeft = new Position(100, 200);
            var topRight = new Position(200, 200);
            var top = Create.Line(topLeft, topRight);
            var left = Create.Line(bottomLeft, topLeft);
            var right = Create.Line(topRight, bottomRight);
            var bottom = Create.Line(bottomLeft, bottomRight);
            var connector = CurveConnector.Create();
            var expectedOrder = new[] { top.NXOpenTag, left.NXOpenTag, bottom.NXOpenTag, right.NXOpenTag };
            var actualOrder = connector.GetPolylineFromStartPoint(top)
                .Select(curve => curve.NXOpenTag)
                .ToArray();
            CollectionAssert.AreEqual(expectedOrder,actualOrder);
           Snap.NX.NXObject.Delete(top, left, right, bottom);
        }
        
        [Test]
        public void Test_GetPolylineFromEndPoint_Valid()
        {
            var bottomLeft = new Position(100, 100);
            var bottomRight = new Position(200, 100);
            var topLeft = new Position(100, 200);
            var topRight = new Position(200, 200);
            var top = Create.Line(topLeft, topRight);
            var left = Create.Line(bottomLeft, topLeft);
            var right = Create.Line(topRight, bottomRight);
            var bottom = Create.Line(bottomLeft, bottomRight);
            var connector = CurveConnector.Create();
            var expectedOrder = new[] {top.NXOpenTag, right.NXOpenTag, bottom.NXOpenTag, left.NXOpenTag};
            var actualOrder = connector.GetPolylineFromEndPoint(top)
                .Select(curve => curve.NXOpenTag)
                .ToArray();
            CollectionAssert.AreEqual(expectedOrder, actualOrder);
            Snap.NX.NXObject.Delete(top, left, right, bottom);
        }

        [Test]
        public void Test_AllPolylines_Valid()
        {
            var curves = Globals.DisplayPart.Curves.Where(curve => curve.Layer == 29).ToArray();
            var connector = CurveConnector.Create(curves);
            Assert.AreEqual(5,connector.GetAllPolylines().Count());
        }
    }
}