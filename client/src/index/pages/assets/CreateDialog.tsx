import * as Mui from '@mui/material';
import * as React from 'react';

import { AssetDto, AssetTypeDto } from '../../api/api';
import Input from '../../components/cards/Input';

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
      type: new AssetTypeDto({ id: -1, code: '', name: '' }),
    })
  );

  const theme = Mui.useTheme();
  const fullScreen = Mui.useMediaQuery(theme.breakpoints.down('md'));

  const onAssetCreate = () => onCreate(new AssetDto());

  return (
    <Mui.Dialog
      open={isOpen}
      onClose={onClose}
      fullScreen={fullScreen}
      classes={{ paper: 'md:min-w-[50vw] lg:min-w-[40vw] 2xl:min-w-[30w]' }}
    >
      <Mui.DialogContent dividers>
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
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={onAssetCreate}>Create asset</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
