// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.BrowserTesting;
using Microsoft.AspNetCore.Components.E2ETest.Infrastructure.ServerFixtures;
using Microsoft.AspNetCore.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Components.E2ETest.Infrastructure
{
    public abstract class ServerTestBase<TServerFixture>
        : ComponentBrowserTestBase,
        IClassFixture<TServerFixture>
        where TServerFixture: ServerFixture
    {
        public string ServerPathBase => "/subdir";

        protected readonly TServerFixture _serverFixture;

        public ServerTestBase(
            TServerFixture serverFixture,
            ITestOutputHelper output)
            : base(output)
        {
            _serverFixture = serverFixture;
        }

        public void Navigate(string relativeUrl, bool noReload = false)
        {
            //Browser.Navigate(_serverFixture.RootUri, relativeUrl, noReload);
        }

        protected override async Task InitializeCoreAsync(TestContext context)
        {
            await base.InitializeCoreAsync(context);

            if (BrowserManager.IsAvailable(BrowserKind.Chromium))
            {
                await using var browser = await BrowserManager.GetBrowserInstance(BrowserKind.Chromium, BrowserContextInfo);
                var page = await browser.NewPageAsync();
                // Clear logs - we check these during tests in some cases.
                // Make sure each test starts clean.
                await page.EvaluateAsync("console.clear()");
                await page.CloseAsync();
            }
        }
    }
}
