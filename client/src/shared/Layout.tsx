import * as React from 'react';

import NavigationBar from './NavigationBar';

interface Props {
  children: React.ReactNode;
  sidebar?: React.ReactNode;
}

function Layout({ children, sidebar }: Props) {
  return (
    <div className="h-screen flex flex-col flex-grow bg-opacity-20">
      <NavigationBar />
      <div className="flex-grow flex flex-row">
        {sidebar === undefined ? null : sidebar}
        <div className="flex-grow flex flex-col">
          <div className="overflow-x-hidden flex-grow px-12 py-8">{children}</div>
          <div className="mt-6 mb-4 mx-3 text-sm text-stone-400">
            <span className="px-5 cursor-default hover:text-stone-600">Privacy</span>
            <span className="px-5 cursor-default hover:text-stone-600">Terms & Conditions</span>
            <span className="px-5 cursor-default hover:text-stone-600">About</span>
          </div>
        </div>
      </div>
    </div>
  );
}

Layout.defaultProps = { sidebar: false };

export default Layout;
