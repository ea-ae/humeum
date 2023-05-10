import * as React from 'react';

// should we scrap this in favor of Context?
const cache: { [key: number | string]: unknown } = {};

// eslint-disable-next-line no-shadow
export enum CacheKey {
  Transactions = 'transactions',
  TaxSchemes = 'taxSchemes',
  Assets = 'assets',
}

function isCacheInitializationFunction<T>(updateCache: (() => T) | T): updateCache is () => T {
  return typeof updateCache === 'function';
}

/**
 * A hook that stores data in an in-memory cache. Data is persisted between component mounts. Use a unique key for each component instance.
 * @param key Key in which cache data will be stored. If the key is already in use, the data will be returned from the cache.
 * @param setInitial Initial cache value or a function that returns the initial cache value.
 * @returns A tuple containing the cache value and a function to update the cache value.
 */
export default function useCache<T>(key: number | string, setInitial: (() => T) | T) {
  // initialize cache state for component
  const [state, setState] = React.useState<T>(() => {
    if (cache[key] !== undefined) {
      return cache[key] as T; // key is in cache, use it as initial state
    }

    // key is not in cache, add it
    if (isCacheInitializationFunction<T>(setInitial)) {
      cache[key] = setInitial();
    } else {
      cache[key] = setInitial;
    }

    return cache[key] as T;
  });

  const updateCache = (newState: T) => {
    cache[key] = newState;
    setState(newState);
  };

  return [state, updateCache] as const;
}
