import * as React from 'react';

// should we instead use a context here?
const cache: { [key: string]: unknown } = {};

function isUpdateCacheFunction<T>(updateCache: (() => T) | T): updateCache is () => T {
  return typeof updateCache === 'function';
}

function useCache<T>(key: string, setInitial: (() => T) | T) {
  const [state, setState] = React.useState<T>(() => {
    if (cache[key] !== undefined) {
      return cache[key] as T;
    }

    if (isUpdateCacheFunction<T>(setInitial)) {
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

export default useCache;
