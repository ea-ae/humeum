import * as Mui from '@mui/material';
import * as React from 'react';

type Props = {
  tooltip?: string;
  defaultValue: string;
  typePattern: RegExp;
  validPattern: RegExp;
  symbol?: string | null;
  isOutlined?: boolean;

  label: string;
  id?: string | undefined;
  disabled?: boolean;
  fullWidth?: boolean;
  className?: string;

  onChange?: (input: string) => void;
};

export default function Input(props: Props) {
  const { tooltip, defaultValue, typePattern, validPattern, symbol, isOutlined, onChange } = props; // Input props
  const { id, label, disabled, fullWidth } = props; // TextField props (todo: do not use Mui.TextFieldProps bc it has too many)
  const textFieldStyle = `flex-grow my-2 ${props.className}` ?? '';

  const [value, setValue] = React.useState<string>(defaultValue);
  const [isValid, setIsValid] = React.useState<boolean>(validPattern.test(value));
  const [hasChanged, setHasChanged] = React.useState<boolean>(false);

  React.useEffect(() => setValue(defaultValue), [defaultValue]);

  const validate = (e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => {
    e.preventDefault();
    if (typePattern.test(e.target.value)) {
      setValue(e.target.value);
      setIsValid(validPattern.test(e.target.value));
      setHasChanged(true);

      if (validPattern.test(e.target.value) && onChange !== null && onChange !== undefined) {
        onChange(e.target.value);
      }
    }
  };

  return (
    <Mui.Tooltip title={tooltip} placement="top">
      <Mui.TextField
        value={value}
        id={id}
        label={label}
        variant={isOutlined ? 'outlined' : 'standard'}
        disabled={disabled}
        fullWidth={fullWidth}
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
  isOutlined: false,
  onChange: undefined,
  id: undefined,
  disabled: false,
  fullWidth: false,
  className: '',
};
