
export function  RetDescontoBase(sku, qtde) {
      for(let i=0; i < sku.faixasDesconto.length; i = i + 1) {
        let o = sku.faixasDesconto[i]
        if (qtde >= o.quantidadeMinima)
          if (qtde <= o.quantidadeMaxima)
            return o.condicaoComercial.percentualDescontoBase;
      }
      return 0;
    }

export function numberValue(input) {
  const cleanedInput = input.replace(/[,.]/g, '');
  const numberValue = parseFloat(cleanedInput);
  if (isNaN(numberValue)) {
    return "Invalid input";
  }
  return numberValue / 100;
}

export function formatMonetaryValue(input) {
  if (typeof input !== 'string') {
    return "Invalid input";
  }
  const cleanedInput = input.replace(/[,.]/g, '');
  const numberValue = parseFloat(cleanedInput);
  if (isNaN(numberValue)) {
    return "Invalid input";
  }
  const monetaryValue = parseFloat(numberValue / 100)
  const formattedValue = monetaryValue.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  });
  return formattedValue;
}

export  function applyDiscountAndFormat(input, discountPercentage) {
      if (typeof input !== 'string') {
        return "Invalid input";
      }
      const cleanedInput = input.replace(/[,.]/g, '');
      const numberValue = parseFloat(cleanedInput);
      if (isNaN(numberValue)) {
        return "Invalid input";
      }
      let monetaryValue = numberValue / 100;
      if (discountPercentage > 0 && discountPercentage <= 100) {
        const discountFactor = 1 - discountPercentage / 100;
        monetaryValue *= discountFactor;
      }
      const formattedValue = monetaryValue.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      });
      return formattedValue;
    }