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
  FormDescription,
} from "../ui/form";
import { cn } from "@/lib/utils";

export type FieldProps<TFormValues extends FieldValues> = {
  className?: string;
  name: Path<TFormValues>;
  control: Control<TFormValues>;
  label: string;
  description?: React.ReactNode;
  render: (values: {
    field: ControllerRenderProps<TFormValues, Path<TFormValues>>;
    fieldState: ControllerFieldState;
    formState: UseFormStateReturn<TFormValues>;
  }) => React.ReactNode;
};

export function Field<TFormValues extends FieldValues>({
  className,
  name,
  control,
  label,
  description,
  render,
}: FieldProps<TFormValues>) {
  return (
    <FormField
      control={control}
      name={name}
      render={(values) => (
        <FormItem className={cn("flex-1", className)}>
          <FormLabel>{label}</FormLabel>
          <FormControl>{render(values)}</FormControl>
          {description && <FormDescription>{description}</FormDescription>}
          <FormMessage />
        </FormItem>
      )}
    />
  );
}
