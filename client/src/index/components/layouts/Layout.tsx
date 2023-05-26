import * as React from 'react';

import AddItemDial from './AddItemDial';
import NavigationBar from './NavigationBar';

interface Props {
  children: React.ReactNode;
  sidebar?: React.ReactNode;
  centerFooter?: boolean;
}

export default function Layout({ children, sidebar, centerFooter }: Props) {
  const appLayout = sidebar !== undefined;
  const footerLayout = centerFooter ? 'text-center' : '';

  return (
    <div className="h-screen flex flex-col flex-grow">
      <NavigationBar appLayout={appLayout} />
      <div className="flex-grow flex flex-row">
        {appLayout ? sidebar : null}
        <div className="overflow-x-auto flex-grow flex flex-col">
          <div className="flex-grow px-6 lg:px-12 pt-4 lg:pt-8">{children}</div> {/* overflow-x-hidden was here */}
          <div className={`mt-6 mb-4 mx-7 text-sm text-stone-500 ${footerLayout}`}>
            <span className="px-5 whitespace-nowrap cursor-default hover:text-stone-800">Privacy</span>
            <span className="px-5 whitespace-nowrap cursor-default hover:text-stone-800">Terms & Conditions</span>
            <span className="px-5 whitespace-nowrap cursor-default hover:text-stone-800">About</span>
          </div>
        </div>
      </div>
      {appLayout ? <AddItemDial /> : null}
    </div>
  );
}

Layout.defaultProps = {
  sidebar: undefined,
  centerFooter: false,
};
