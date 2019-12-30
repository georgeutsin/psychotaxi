using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CircularObjectPoolTest
    {
        [Test]
        public void SimpleTest()
        {
            CircularObjectPool pool = new CircularObjectPool(new GameObject(), null, 2);
            GameObject o1 = pool.Next();
            GameObject o2 = pool.Next();
            Assert.That(o1 == pool.First());

            pool.ReturnFirst();
            Assert.That(o2 == pool.First());

        }

        [Test]
        public void EmptyTest()
        {
            CircularObjectPool pool = new CircularObjectPool(new GameObject(), null, 0);
            GameObject o1 = pool.Next();
            GameObject o2 = pool.Next();
            GameObject o3 = pool.Next();
            Assert.That(o1 == pool.First());

            pool.ReturnFirst();
            Assert.That(o2 == pool.First());

            pool.ReturnFirst();
            Assert.That(o3 == pool.First());
        }

        [Test]
        public void LifecycleTest()
        {
            CircularObjectPool pool = new CircularObjectPool(new GameObject(), null, 3);
            GameObject o1 = pool.Next();
            GameObject o2 = pool.Next();

            Assert.That(o1 == pool.First());

            pool.ReturnFirst();
            Assert.That(o2 == pool.First());

            GameObject o3 = pool.Next();
            GameObject o4 = pool.Next();
            GameObject o5 = pool.Next();
            Assert.That(o2 == pool.First());

            pool.ReturnFirst();
            Assert.That(o3 == pool.First());

            pool.ReturnFirst();
            Assert.That(o4 == pool.First());

            pool.ReturnFirst();
            Assert.That(o5 == pool.First());
        }
    }
}
