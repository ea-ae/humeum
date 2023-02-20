import * as Mui from '@mui/material';
import * as React from 'react';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: string;
  disabled?: boolean;
  typePattern: RegExp;
  validPattern: RegExp;
  symbol?: string | null;
}

function Input({ label, tooltip, defaultValue, disabled, typePattern, validPattern, symbol }: Props) {
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
    <Mui.Tooltip title={tooltip} placement="top">
      <Mui.TextField
        disabled={disabled}
        className="flex-grow my-4 mr-8"
        id="return"
        label={label}
        variant="standard"
        value={value}
        onChange={(e) => validate(e)}
        InputLabelProps={{ className: isValid ? '' : 'text-red-600 border-red-500' }}
        InputProps={
          symbol === null ? {} : { endAdornment: <Mui.InputAdornment position="end">{symbol}</Mui.InputAdornment> }
        }
      />
    </Mui.Tooltip>
  );
}

Input.defaultProps = {
  tooltip: '',
  disabled: false,
  symbol: null,
};

export default Input;
