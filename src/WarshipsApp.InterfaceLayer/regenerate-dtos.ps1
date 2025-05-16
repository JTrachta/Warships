Push-Location $PSScriptRoot

$contracts_file='Oas/WarshipsContract.yaml'
$namespace='WarshipsApp.InterfaceLayer.Dtos'
$output_cs='Dtos/Generated/Dtos.Generated.cs'
$date_time_type='System.DateTime'

Push-Location ''

docker run --rm -it -v ${PWD}:/local countingup/nswag `
    openapi2csclient `
    /input:/local/${contracts_file} `
    /namespace:${namespace} `
    /DateType:${date_time_type} `
    /DateTimeType:${date_time_type} `
    /GenerateContractsOutput:true `
    /ContractsOutput:/local/${output_cs} `
    /ContractsNamespace:${namespace} `
    /GenerateClientClasses:false

Pop-Location
Pop-Location
