//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks; 
//using CTS_Library.Comparators;
//using FastClass;
//using NUnit.Framework;
//using Snap;

//namespace TestCurveConnector
//{
//    [TestFixture]
//    public class TestChainClass
//    {
//        public static EqualityPosition _equalityPosition = new EqualityPosition();
        
//        [SetUp]
//        public void SetUp()
//        {
//            Globals.DisplayPart = Snap.NX.Part.OpenPart("P:\\CurveConnectorTest.prt");
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            Globals.DisplayPart.Close(true, true);
//        }

//        private static Position position111 = new Position(1, 1, 1);
//        private static Position position222 = new Position(2, 2, 2);
//        private static Position position333 = new Position(3, 3, 3);
//        private static Position position444 = new Position(4, 4, 4);


//        /// <summary>
//        /// Tests:
//        /// 1. Makes sure that the empty constructor doesn't throw an exception.
//        /// 2. Both HeadNode and TailNode are null.
//        /// 3. Both UnusedHeadPosition and UnusedTailPosition are default.
//        /// 4. 
//        /// </summary>
//        [Test]
//        public void Test_EmptyConstructor()
//        {
//            // ReSharper disable once ObjectCreationAsStatement
//            Assert.DoesNotThrow(() =>
//            {
//                var chain =new Chain();
//                Assert.AreEqual(null,chain.Head);
//                Assert.AreEqual(null, chain.Tail);
//                Assert.AreEqual(default(Position), chain.UnusedHeadPosition);
//                Assert.AreEqual(default(Position), chain.UnusedTailPosition);
//            });
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        [Test]
//        public void Test_FirstAdd()
//        {
//            var line = Create.Line(position111, position222);
//            var chain = new Chain();
//            chain.AddToTail(line);

//            // Tests UnusedHeadPosition
//            var expected = _equalityPosition.GetHashCode(position222);
//            var actual = _equalityPosition.GetHashCode(chain.UnusedHeadPosition);
//            var errorMessage = "UnusedHead (Expected: " + position222 + " Actual: " + chain.UnusedHeadPosition + ")";
//            Assert.AreEqual(expected, actual,errorMessage);

//            // Tests UnusedTailPosition
//            expected = _equalityPosition.GetHashCode(position111);
//            actual = _equalityPosition.GetHashCode(chain.UnusedTailPosition);
//            errorMessage = "UnusedTail (Expected: " + position111 + " Actual: " + chain.UnusedTailPosition + ")";
//            Assert.AreEqual(expected, actual, errorMessage);

//            Assert.IsTrue(chain.Head != null,"Expected: \"Head\" to be not null, but it was.");
//            Assert.IsTrue(chain.Tail == null, "Expected: \"Tail\" to be null, but it wasn't.");
//            Snap.NX.NXObject.Delete(line);
//        }

//        [Test]
//        public void Test_SecondAdd()
//        {
//            var line1 = Create.Line(position111, position222);
//            var line2 = Create.Line(position222, position333);
//            var chain = new Chain();
//            chain.AddToTail(line1);
//            chain.AddToTail(line2);

//            // Tests UnusedHeadPosition
//            var expected = _equalityPosition.GetHashCode(position111);
//            var actual = _equalityPosition.GetHashCode(chain.UnusedHeadPosition);
//            var errorMessage = "UnusedHead (Expected: " + position111 + " Actual: " + chain.UnusedHeadPosition + ")";
//            Assert.AreEqual(expected, actual, errorMessage);

//            // Tests UnusedTailPosition
//            expected = _equalityPosition.GetHashCode(position333);
//            actual = _equalityPosition.GetHashCode(chain.UnusedTailPosition);
//            errorMessage = "UnusedTail (Expected: " + position333 + " Actual: " + chain.UnusedTailPosition + ")";
//            Assert.AreEqual(expected, actual, errorMessage);

//            Assert.IsTrue(chain.Head != null, "Expected: \"Head\" to be not null, but it was.");
//            Assert.IsTrue(chain.Tail != null, "Expected: \"Tail\" to be not null, but it was.");

//            Snap.NX.NXObject.Delete(line1,line2);
//        }
//    }
//}