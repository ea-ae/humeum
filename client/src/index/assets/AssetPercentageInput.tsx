import * as Mui from '@mui/material';
import * as React from 'react';

interface Props {
  label: string;
  defaultValue: string;
  disabled?: boolean;
}

function AssetPercentageInput({ label, defaultValue, disabled }: Props) {
  const typePattern = /^[0-9]{0,3}($|[,.][0-9]{0,2}$)/;
  const validPattern = /^[0-9]+($|[,.][0-9]{1,2})$/;

  const [value, setValue] = React.useState<string>(defaultValue);
  const [isValid, setIsValid] = React.useState<boolean>(validPattern.test(value));

  const validate = (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    if (typePattern.test(e.target.value)) {
      setValue(e.target.value);
      setIsValid(validPattern.test(e.target.value));
    }
  };

  return (
    <Mui.TextField
      disabled={disabled}
      className="flex-grow my-4 mr-8"
      id="return"
      label={label}
      variant="standard"
      value={value}
      onChange={(e) => validate(e)}
      InputProps={{ endAdornment: <Mui.InputAdornment position="end">%</Mui.InputAdornment> }}
      InputLabelProps={{ className: isValid ? '' : 'text-red-600 border-red-500' }}
    />
  );
}

AssetPercentageInput.defaultProps = {
  disabled: false,
};

export default AssetPercentageInput;
