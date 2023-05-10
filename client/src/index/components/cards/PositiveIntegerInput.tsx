import Input from './Input';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: number | null;
  disabled?: boolean;
  className?: string;

  onChange?: undefined | ((percentage: number | null) => void);
}

const typePattern = /^[0-9]{0,3}$/;
const validPattern = /^0*[1-9][0-9]{0,2}$/;

export default function PositiveIntegerInput({ label, tooltip, defaultValue, disabled, className, onChange }: Props) {
  const onInputChange = (input: string) => {
    if (onChange !== null && onChange !== undefined) {
      onChange(input === '' ? null : parseInt(input, 10));
    }
  };

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue?.toString() ?? ''}
      typePattern={typePattern}
      validPattern={validPattern}
      disabled={disabled}
      className={className}
      onChange={onInputChange}
    />
  );
}

PositiveIntegerInput.defaultProps = {
  tooltip: '',
  disabled: false,
  className: '',
  onChange: undefined,
};
