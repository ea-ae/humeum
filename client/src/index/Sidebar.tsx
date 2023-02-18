import * as React from 'react';

import SidebarTab from './SidebarTab';

interface Props {
  activeTab: number;
}

function Sidebar({ activeTab }: Props) {
  return (
    <div className="shadow-inner bg-secondary-400">
      <SidebarTab label="Home" path="/" active={activeTab === 0} />
      <SidebarTab
        label="Transactions"
        path="/transactions"
        active={activeTab === 1}
      />
      <SidebarTab label="Assets" path="/assets" active={activeTab === 2} />
      <SidebarTab label="Taxes" path="/taxes" active={activeTab === 3} />
      <SidebarTab label="Settings" path="/settings" active={activeTab === 4} />
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
