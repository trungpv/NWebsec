﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;
using NUnit.Framework;
using NWebsec.SessionSecurity.Crypto;

namespace NWebsec.SessionSecurity.Tests.Unit.Crypto
{
    [TestFixture]
    public class KbkdfHmacSha256CtrTests
    {
        private KbkdfHmacSha256Ctr _kdf;

        [SetUp]
        public void Setup()
        {
            _kdf = new KbkdfHmacSha256Ctr();
        }

        [Test]
        public void DeriveKey_GeneratesDifferentKeysForDifferentLabels()
        {
            const int keyLength = 128;
            var ki = new byte[16];
            
            
            var derivedKey = _kdf.DeriveKey(keyLength, ki, "label");
            var secondDerivedKey = _kdf.DeriveKey(keyLength, ki, "label2");

            Assert.AreNotEqual(derivedKey,secondDerivedKey);
        }

        [Test]
        public void DeriveKey_GeneratesDifferentKeyWithContext()
        {
            const int keyLength = 128;
            var ki = new byte[16];


            var derivedKey = _kdf.DeriveKey(keyLength, ki, "label");
            var secondDerivedKey = _kdf.DeriveKey(keyLength, ki, "label", "context");

            Assert.AreNotEqual(derivedKey, secondDerivedKey);
        }

        [Test]
        public void DeriveKey_RequestedKeyLengthMax_NoException()
        {
            const int keyLength = 255*256;
            var ki = new byte[16];

            Assert.DoesNotThrow(() =>  _kdf.DeriveKey(keyLength, ki, "label"));
        }

        [Test]
        public void DeriveKey_RequestedKeyLengthTooLong_ThrowsException()
        {
            const int keyLength = (255 * 256)+1;
            var ki = new byte[16];
            
            Assert.Throws<ArgumentException>(() => _kdf.DeriveKey(keyLength, ki, "label"));
        }
    }
}
