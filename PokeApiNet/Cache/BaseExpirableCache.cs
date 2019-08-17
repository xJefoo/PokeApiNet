﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace PokeApiNet.Cache
{
    internal abstract class BaseExpirableCache
    {
        private CancellationTokenSource ClearToken { get; set; } = new CancellationTokenSource();

        protected void ExpireAll()
        {
            // TODO add lock?
            if (ClearToken != null && !ClearToken.IsCancellationRequested && ClearToken.Token.CanBeCanceled)
            {
                ClearToken.Cancel();
                ClearToken.Dispose();
            }

            ClearToken = new CancellationTokenSource();
        }

        /// <summary>
        /// Gets the <see cref="MemoryCacheEntryOptions"/> instance.
        /// </summary>
        /// <remarks>
        /// New options instance has to be constantly instantiated instead of shared
        /// as a consequence of <see cref="ClearToken"/> being mutable
        /// </remarks>
        protected MemoryCacheEntryOptions CacheEntryOptions => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(ClearToken.Token));
    }
}