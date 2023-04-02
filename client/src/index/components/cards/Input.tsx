import * as Mui from '@mui/material';
import * as React from 'react';

type Props = Mui.TextFieldProps & {
  tooltip?: string;
  defaultValue: string;
  typePattern: RegExp;
  validPattern: RegExp;
  symbol?: string | null;
};

export default function Input(props: Props) {
  const { tooltip, defaultValue, typePattern, validPattern, symbol } = props; // Input props
  const { id, label, variant, disabled, fullWidth } = props; // TextField props
  const textFieldStyle = `block flex-grow my-2 mr-8 ${props.className}` ?? '';

  const [value, setValue] = React.useState<string>(defaultValue);
  const [isValid, setIsValid] = React.useState<boolean>(validPattern.test(value));
  const [hasChanged, setHasChanged] = React.useState<boolean>(false);

  const validate = (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    if (typePattern.test(e.target.value)) {
      setValue(e.target.value);
      setIsValid(validPattern.test(e.target.value));
      setHasChanged(true);
    }
  };

  return (
    <Mui.Tooltip title={tooltip} placement="top">
      <Mui.TextField
        value={value}
        id={id}
        label={label}
        variant={variant ?? 'standard'}
        disabled={disabled}
        fullWidth
        className={textFieldStyle}
        onChange={(e) => validate(e)}
        InputLabelProps={{ className: isValid || !hasChanged ? '' : 'text-red-600 border-red-500' }}
        InputProps={symbol === null ? {} : { endAdornment: <Mui.InputAdornment position="end">{symbol}</Mui.InputAdornment> }}
      />
    </Mui.Tooltip>
  );
}

Input.defaultProps = {
  tooltip: '',
  symbol: null,
};
