import AccountBalanceOutlinedIcon from '@mui/icons-material/AccountBalanceOutlined';
import HomeWorkOutlinedIcon from '@mui/icons-material/HomeWorkOutlined';
import ReceiptLongOutlinedIcon from '@mui/icons-material/ReceiptLongOutlined';
import SavingsOutlinedIcon from '@mui/icons-material/SavingsOutlined';
import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';

import SidebarTab from './SidebarTab';

interface Props {
  activeTabLabel: string;
}

export default function Sidebar({ activeTabLabel }: Props) {
  return (
    <div className="min-w-fit shadow-inner text-gray-900 bg-secondary-100 border-solid border-secondary-400 border-r-4">
      <SidebarTab label="Home" activeLabel={activeTabLabel} icon={<HomeWorkOutlinedIcon />} path="/" />
      <SidebarTab label="Transactions" activeLabel={activeTabLabel} icon={<ReceiptLongOutlinedIcon />} path="/transactions" />
      <SidebarTab label="Assets" activeLabel={activeTabLabel} icon={<SavingsOutlinedIcon />} path="/assets" />
      <SidebarTab label="Taxes" activeLabel={activeTabLabel} icon={<AccountBalanceOutlinedIcon />} path="/taxes" />
      <SidebarTab label="Settings" activeLabel={activeTabLabel} icon={<SettingsOutlinedIcon />} path="/settings" />
    </div>
  );
}
