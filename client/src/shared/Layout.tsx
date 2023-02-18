import * as React from 'react';

import NavigationBar from './NavigationBar';

interface Props {
  children: React.ReactNode;
  sidebar?: React.ReactNode;
}

function Layout({ children, sidebar }: Props) {
  return (
    <div className="h-screen flex flex-col flex-grow bg-stone-100 bg-opacity-20">
      <NavigationBar />
      <div className="flex-grow flex flex-row">
        {sidebar === undefined ? null : sidebar}
        <div className="flex-grow px-12 py-8">{children}</div>
      </div>
    </div>
  );
}

Layout.defaultProps = { sidebar: false };

export default Layout;
