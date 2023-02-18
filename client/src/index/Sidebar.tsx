import * as React from 'react';

import SidebarTab from './SidebarTab';

interface Props {
  activeTabLabel: string;
}

function Sidebar({ activeTabLabel }: Props) {
  return (
    <div className="shadow-inner bg-secondary-400">
      <SidebarTab label="Home" activeLabel={activeTabLabel} path="/" />
      <SidebarTab label="Transactions" activeLabel={activeTabLabel} path="/transactions" />
      <SidebarTab label="Assets" activeLabel={activeTabLabel} path="/assets" />
      <SidebarTab label="Taxes" activeLabel={activeTabLabel} path="/taxes" />
      <SidebarTab label="Settings" activeLabel={activeTabLabel} path="/settings" />
    </div>
  );

  // return (
  //     <Mui.Box className="shadow-inner bg-secondary-400">
  //         <Mui.Tabs TabIndicatorProps={{className: 'w-0'}}
  //                   value={navSelection}
  //                   onChange={handleNavigationChange}
  //                   orientation="vertical"
  //                   variant="scrollable"
  //                   scrollButtons="auto"
  //         >
  //             {/* <ReactRouter.Link to="/"> */}
  //                 <Mui.Tab className="px-7 text-gray-100 hover:text-white"
  //                          classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' }}
  //                          label="Home"
  //                          component={ReactRouter.Link} to="/transactions" />
  //             {/* </ReactRouter.Link> */}

  //             <Mui.Tab className="px-7 text-gray-100 hover:text-white"
  //                      classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' }}
  //                      label="Transactions" />
  //             <Mui.Tab className="px-7 text-gray-100 hover:text-white"
  //                      classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' }}
  //                      label="Assets" />
  //             <Mui.Tab className="px-7 text-gray-100 hover:text-white"
  //                      classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' }}
  //                      label="Taxes" />
  //             <Mui.Tab className="px-7 text-gray-100 hover:text-white"
  //                      classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' }}
  //                      label="Settings" />
  //         </Mui.Tabs>
  //     </Mui.Box>
  // );
}

export default Sidebar;
