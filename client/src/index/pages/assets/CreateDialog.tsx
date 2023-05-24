import * as Mui from '@mui/material';
import * as React from 'react';

import { AssetDto, AssetTypeDto } from '../../api/api';
import Input from '../../components/cards/Input';

interface AssetType {
  id: number;
  name: string;
  code: string;
}

const ASSET_TYPES: AssetType[] = [
  { id: 1, name: 'Liquid/Cash', code: 'LIQUID' },
  { id: 2, name: 'Index fund', code: 'INDEX' },
  { id: 3, name: 'Managed fund', code: 'MANAGED' },
  { id: 4, name: 'Real estate', code: 'REALESTATE' },
  { id: 5, name: 'Bond', code: 'BOND' },
  { id: 6, name: 'Stock/Derivative', code: 'STOCK' },
  { id: 7, name: 'Other', code: 'OTHER' },
];

interface Props {
  isOpen: boolean;

  onClose: () => void;
  onCreate: (asset: AssetDto) => void;
}

export default function CreateDialog({ isOpen, onClose, onCreate }: Props) {
  const [data, setData] = React.useState<AssetDto>(
    new AssetDto({
      id: -1,
      name: '',
      description: '',
      returnRate: 5,
      standardDeviation: 5,
      default: false,
      type: new AssetTypeDto({ id: 7, code: 'OTHER', name: 'Other' }),
    })
  );

  const theme = Mui.useTheme();
  const fullScreen = Mui.useMediaQuery(theme.breakpoints.down('md'));

  const onAssetCreate = () => {
    data.name ??= 'Unnamed asset';
    onClose();
    onCreate(data);
  };

  const setAssetType = (event: Mui.SelectChangeEvent<number>) => {
    const id = event.target.value as number;
    const assetType = ASSET_TYPES.find((at) => at.id === id);
    if (assetType === undefined) throw new Error(`Asset type with id ${data.type.id} not found`);

    setData(new AssetDto({ ...data, type: new AssetTypeDto({ id: assetType.id, code: assetType.code, name: assetType.name }) }));
  };

  return (
    <Mui.Dialog
      open={isOpen}
      onClose={onClose}
      fullScreen={fullScreen}
      classes={{ paper: 'md:min-w-[50vw] lg:min-w-[40vw] 2xl:min-w-[30w]' }}
    >
      <Mui.DialogContent className="flex flex-col" dividers>
        <Input
          label="Name"
          defaultValue={data.name}
          typePattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,50}$/}
          validPattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{1,50}$/}
          isOutlined
          onChange={(value: string) => setData(new AssetDto({ ...data, name: value }))}
        />
        <Input
          label="Description"
          defaultValue={data.description ?? ''}
          typePattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}$/}
          validPattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}$/}
          className="md:col-span-2"
          isOutlined
          onChange={(value: string) => setData(new AssetDto({ ...data, description: value }))}
        />
        <Mui.Select value={data.type.id} className="my-2" onChange={setAssetType}>
          {ASSET_TYPES.map((assetType) => (
            <Mui.MenuItem value={assetType.id} key={assetType.id}>
              {assetType.name}
            </Mui.MenuItem>
          ))}
        </Mui.Select>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={onAssetCreate}>Create asset</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
