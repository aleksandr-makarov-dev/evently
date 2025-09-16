import type {
  FieldValues,
  Path,
  Control,
  ControllerRenderProps,
  ControllerFieldState,
  UseFormStateReturn,
} from "react-hook-form";
import {
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "../ui/form";

export type FieldProps<TFormValues extends FieldValues> = {
  name: Path<TFormValues>;
  control: Control<TFormValues>;
  label: string;
  render: (values: {
    field: ControllerRenderProps<TFormValues, Path<TFormValues>>;
    fieldState: ControllerFieldState;
    formState: UseFormStateReturn<TFormValues>;
  }) => React.ReactNode;
};

export function Field<TFormValues extends FieldValues>({
  name,
  control,
  label,
  render,
}: FieldProps<TFormValues>) {
  return (
    <FormField
      control={control}
      name={name}
      render={(values) => (
        <FormItem className="flex-1">
          <FormLabel>{label}</FormLabel>
          <FormControl>{render(values)}</FormControl>
          <FormMessage />
        </FormItem>
      )}
    />
  );
}
