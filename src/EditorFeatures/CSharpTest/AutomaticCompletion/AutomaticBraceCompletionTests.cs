﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis.Editor.Implementation.AutomaticCompletion;
using Microsoft.CodeAnalysis.Editor.Shared.Options;
using Microsoft.CodeAnalysis.Editor.UnitTests.AutomaticCompletion;
using Microsoft.CodeAnalysis.Editor.UnitTests.Workspaces;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.AutomaticCompletion
{
    public class AutomaticBraceCompletionTests : AbstractAutomaticBraceCompletionTests
    {
        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Creation()
        {
            using (var session = CreateSession("$$"))
            {
                Assert.NotNull(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_String()
        {
            var code = @"class C
{
    string s = ""$$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_String2()
        {
            var code = @"class C
{
    string s = @""
$$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString1()
        {
            var code = @"class C
{
    string s = $""$$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString2()
        {
            var code = @"class C
{
    string s = $@""$$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString3()
        {
            var code = @"class C
{
    string x = ""foo""
    string s = $""{x} $$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString4()
        {
            var code = @"class C
{
    string x = ""foo""
    string s = $@""{x} $$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString5()
        {
            var code = @"class C
{
    string s = $""{{$$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void ValidLocation_InterpolatedString6()
        {
            var code = @"class C
{
    string s = $""{}$$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_InterpolatedString1()
        {
            var code = @"class C
{
    string s = @""$$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_InterpolatedString2()
        {
            var code = @"class C
{
    string s = ""$$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_Comment()
        {
            var code = @"class C
{
    //$$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_Comment2()
        {
            var code = @"class C
{
    /* $$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_Comment3()
        {
            var code = @"class C
{
    /// $$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void InvalidLocation_Comment4()
        {
            var code = @"class C
{
    /** $$
}";
            using (var session = CreateSession(code))
            {
                Assert.Null(session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void MultiLine_Comment()
        {
            var code = @"class C
{
    void Method()
    {
        /* */$$
    }
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);
                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void MultiLine_DocComment()
        {
            var code = @"class C
{
    void Method()
    {
        /** */$$
    }
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void String1()
        {
            var code = @"class C
{
    void Method()
    {
        var s = """"$$
    }
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void String2()
        {
            var code = @"class C
{
    void Method()
    {
        var s = @""""$$
    }
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_OpenBrace()
        {
            var code = @"class C $$";

            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_Delete()
        {
            var code = @"class C $$";

            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                CheckBackspace(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_Tab()
        {
            var code = @"class C $$";

            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                CheckTab(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_CloseBrace()
        {
            var code = @"class C $$";

            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                CheckOverType(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Method_OpenBrace_Multiple()
        {
            var code = @"class C
{
    void Method() { $$
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_OpenBrace_Enter()
        {
            var code = @"class C $$";

            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                CheckReturn(session.Session, 4);
            }
        }

        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void Class_ObjectInitializer_OpenBrace_Enter()
        {
            var code = @"using System.Collections.Generic;
 
class C
{
    List<C> list = new List<C>
    {
        new C $$
    };
}";

            var expected = @"using System.Collections.Generic;
 
class C
{
    List<C> list = new List<C>
    {
        new C
        {

        }
    };
}";
            using (var session = CreateSession(code))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                CheckReturn(session.Session, 12, expected);
            }
        }

        [WorkItem(850540)]
        [Fact, Trait(Traits.Feature, Traits.Features.AutomaticCompletion)]
        public void BlockIndentationWithAutomaticBraceFormattingDisabled()
        {
            var code = @"class C
{
    public void X()
    $$
}";

            var expected = @"class C
{
    public void X()
    {}
}";

            var optionSet = new Dictionary<OptionKey, object>
                            {
                                { new OptionKey(FeatureOnOffOptions.AutoFormattingOnCloseBrace, LanguageNames.CSharp), false },
                                { new OptionKey(FormattingOptions.SmartIndent, LanguageNames.CSharp), FormattingOptions.IndentStyle.Block }
                            };
            using (var session = CreateSession(code, optionSet))
            {
                Assert.NotNull(session);

                CheckStart(session.Session);
                Assert.Equal(expected, session.Session.SubjectBuffer.CurrentSnapshot.GetText());

                CheckReturnOnNonEmptyLine(session.Session, 3);
            }
        }

        internal Holder CreateSession(string code, Dictionary<OptionKey, object> optionSet = null)
        {
            return CreateSession(
                CSharpWorkspaceFactory.CreateWorkspaceFromFile(code),
                BraceCompletionSessionProvider.CurlyBrace.OpenCharacter, BraceCompletionSessionProvider.CurlyBrace.CloseCharacter, optionSet);
        }
    }
}
