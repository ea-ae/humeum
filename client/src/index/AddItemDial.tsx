import AccountBalanceOutlinedIcon from '@mui/icons-material/AccountBalanceOutlined';
import ReceiptLongOutlinedIcon from '@mui/icons-material/ReceiptLongOutlined';
import SavingsOutlinedIcon from '@mui/icons-material/SavingsOutlined';
import * as Mui from '@mui/material';

function AddItemDial() {
  return (
    <Mui.SpeedDial ariaLabel="Add Item" className="absolute right-5 bottom-5" icon={<Mui.SpeedDialIcon />}>
      <Mui.SpeedDialAction key="addTaxScheme" icon={<AccountBalanceOutlinedIcon />} tooltipTitle="Add tax scheme" />
      <Mui.SpeedDialAction key="addAsset" icon={<SavingsOutlinedIcon />} tooltipTitle="Add asset type" />
      <Mui.SpeedDialAction key="addTransaction" icon={<ReceiptLongOutlinedIcon />} tooltipTitle="Add transaction" />
    </Mui.SpeedDial>
  );
}

export default AddItemDial;
