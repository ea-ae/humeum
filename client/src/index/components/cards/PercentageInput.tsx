import Input from './Input';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: number;
  disabled?: boolean;
  className?: string;

  onChange?: undefined | ((percentage: number) => void);
}

const typePattern = /^[0-9]{0,3}($|[,.][0-9]{0,2}$)/;
const validPattern = /^[0-9]{1,3}($|[,.][0-9]{1,2})$/;

export default function PercentageInput({ label, tooltip, defaultValue, disabled, className, onChange }: Props) {
  const onInputChange = (input: string) => {
    if (onChange !== null && onChange !== undefined) {
      onChange(parseFloat(input));
    }
  };

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue.toString()}
      symbol="%"
      typePattern={typePattern}
      validPattern={validPattern}
      disabled={disabled}
      className={className}
      onChange={onInputChange}
    />
  );
}

PercentageInput.defaultProps = {
  tooltip: '',
  disabled: false,
  className: '',
  onChange: undefined,
};
