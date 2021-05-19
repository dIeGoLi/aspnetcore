// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.BrowserTesting;
using PlaywrightSharp;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Components.E2ETest.Infrastructure
{
    public class ComponentBrowserTestBase : BrowserTestBase
    {
        public ComponentBrowserTestBase(ITestOutputHelper output = null) : base(output) { }

        protected async Task MountTestComponentAsync<TComponent>(IPage page)
        {
            var componentType = typeof(TComponent);
            var componentTypeName = componentType.Assembly == typeof(BasicTestApp.Program).Assembly ?
                componentType.FullName :
                componentType.AssemblyQualifiedName;
            var testSelector = await page.WaitForSelectorAsync("#test-selector > select");
            Assert.NotNull(testSelector);

            Output.WriteLine("Selecting test: " + componentTypeName);

            var selected = await page.SelectOptionAsync("#test-selector > select", componentTypeName);
            Assert.True(selected.Length == 1);
            Assert.Equal(componentTypeName, selected.First());
        }
    }
}
