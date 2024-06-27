using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Application.Streaming.Dto;
public class FlatDto : BaseDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Description { get; set; }

    private decimal _value;

    [Required(ErrorMessage = "O campo Valor é obrigatório.")]
    public decimal Value
    {
        get => _value;
        set => _value = value;
    }

    [Required(ErrorMessage = "O campo Valor é obrigatório.")]
    public string FormattedValue
    {
        get => _value.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
        set
        {
            if (decimal.TryParse(value, NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"), out decimal result))
            {
                _value = result;
            }
            else
            {
                throw new ArgumentException("A campo valor fornecido não é um valor de moeda válido.");
            }
        }
    }
}