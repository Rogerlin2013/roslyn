﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using Xunit;

namespace Microsoft.CodeAnalysis.UnitTests.Text
{
    public class SourceTextTests
    {
        private static readonly Encoding s_utf8 = Encoding.UTF8;
        private static readonly Encoding s_utf8Bom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        private static readonly Encoding s_unicode = Encoding.Unicode;

        [Fact]
        public void Encoding1()
        {
            Assert.Same(s_utf8, SourceText.From("foo", s_utf8).Encoding);
            Assert.Same(s_unicode, SourceText.From("foo", s_unicode).Encoding);
            Assert.Same(s_unicode, SourceText.From(new MemoryStream(s_unicode.GetBytes("foo")), s_unicode).Encoding);
        }

        [Fact]
        public void EncodingBOM()
        {
            var stream = new MemoryStream(s_utf8Bom.GetPreamble().Concat(s_utf8Bom.GetBytes("abc")).ToArray());
            Assert.Equal(s_utf8.EncodingName, SourceText.From(stream, s_unicode).Encoding.EncodingName);
        }

        [Fact]
        public void ChecksumAlgorithm1()
        {
            Assert.Equal(SourceHashAlgorithm.Sha1, SourceText.From("foo").ChecksumAlgorithm);
            Assert.Equal(SourceHashAlgorithm.Sha1, SourceText.From("foo", checksumAlgorithm: SourceHashAlgorithm.Sha1).ChecksumAlgorithm);
            Assert.Equal(SourceHashAlgorithm.Sha256, SourceText.From("foo", checksumAlgorithm: SourceHashAlgorithm.Sha256).ChecksumAlgorithm);

            var stream = new MemoryStream(s_unicode.GetBytes("foo"));

            Assert.Equal(SourceHashAlgorithm.Sha1, SourceText.From(stream).ChecksumAlgorithm);
            Assert.Equal(SourceHashAlgorithm.Sha1, SourceText.From(stream, checksumAlgorithm: SourceHashAlgorithm.Sha1).ChecksumAlgorithm);
            Assert.Equal(SourceHashAlgorithm.Sha256, SourceText.From(stream, checksumAlgorithm: SourceHashAlgorithm.Sha256).ChecksumAlgorithm);
        }

        [Fact]
        public void ContentEquals()
        {
            var f = SourceText.From("foo", s_utf8);

            Assert.True(f.ContentEquals(SourceText.From("foo", s_utf8)));
            Assert.False(f.ContentEquals(SourceText.From("fooo", s_utf8)));
            Assert.True(SourceText.From("foo", s_utf8).ContentEquals(SourceText.From("foo", s_utf8)));

            var e1 = EncodedStringText.Create(new MemoryStream(s_unicode.GetBytes("foo")), s_unicode);
            var e2 = EncodedStringText.Create(new MemoryStream(s_utf8.GetBytes("foo")), s_utf8);

            Assert.True(e1.ContentEquals(e1));
            Assert.True(f.ContentEquals(e1));
            Assert.True(e1.ContentEquals(f));

            Assert.True(e2.ContentEquals(e2));
            Assert.True(e1.ContentEquals(e2));
            Assert.True(e2.ContentEquals(e1));
        }
    }
}
