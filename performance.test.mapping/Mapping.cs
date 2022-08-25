using AutoMapper;
using BenchmarkDotNet.Attributes;
using MappingTest.Models;
using Mapster;

namespace MappingTest
{
    [MinColumn]
    [MaxColumn]
    [MemoryDiagnoser]
    public class Mapping
    {
        private readonly int N = 1000;
        private readonly Mapper _mapper;

        public Mapping()
        {
            _mapper = SetupAutoMapper();
            SetupMapster();
        }

        [Benchmark]
        public long TestMapster()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < N; i++)
            {
                var sourceDto = new SourceDto();
                var destinationDto = new DestinationDto();
                var testA = new TestDtoA();
                var testB = new TestDtoB();
                var testC = new TestDtoC();

                TypeAdapter.Adapt(sourceDto, destinationDto);
                TypeAdapter.Adapt(sourceDto, testA);
                TypeAdapter.Adapt(sourceDto, testB);
                TypeAdapter.Adapt(sourceDto, testC);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        [Benchmark]
        public long TestAutoMapper()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < N; i++)
            {
                var sourceDto = new SourceDto();
                var destinationDto = new DestinationDto();
                var testA = new TestDtoA();
                var testB = new TestDtoB();
                var testC = new TestDtoC();

                _mapper.Map(sourceDto, destinationDto);
                _mapper.Map(sourceDto, testA);
                _mapper.Map(sourceDto, testB);
                _mapper.Map(sourceDto, testC);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private void SetupMapster()
        {
            TypeAdapterConfig<SourceDto, DestinationDto>.NewConfig();
            TypeAdapterConfig<SourceDto, TestDtoA>.NewConfig()
                .Map(dest => dest.DoubleValue1TestDtoA, source => source.DoubleValue1)
                .Map(dest => dest.DoubleValue2TestDtoA, source => source.DoubleValue2)
                .Map(dest => dest.DoubleValue3TestDtoA, source => source.DoubleValue3)
                .Map(dest => dest.IntValue1TestDtoA, source => source.IntValue1)
                .Map(dest => dest.IntValue2TestDtoA, source => source.IntValue2)
                .Map(dest => dest.IntValue3TestDtoA, source => source.IntValue3)
                .Map(dest => dest.StringValue1TestDtoA, source => source.StringValue1)
                .Map(dest => dest.StringValue2TestDtoA, source => source.StringValue2)
                .Map(dest => dest.StringValue3TestDtoA, source => source.StringValue3)
                .Map(dest => dest.DateTimeValueTestDtoA, source => source.DateTimeValue1);
            TypeAdapterConfig<SourceDto, TestDtoB>.NewConfig()
                .Map(dest => dest.DoubleValue1TestDtoB, source => source.DoubleValue4)
                .Map(dest => dest.DoubleValue2TestDtoB, source => source.DoubleValue5)
                .Map(dest => dest.DoubleValue3TestDtoB, source => source.DoubleValue6)
                .Map(dest => dest.IntValue1TestDtoB, source => source.IntValue4)
                .Map(dest => dest.IntValue2TestDtoB, source => source.IntValue5)
                .Map(dest => dest.IntValue3TestDtoB, source => source.IntValue6)
                .Map(dest => dest.StringValue1TestDtoB, source => source.StringValue4)
                .Map(dest => dest.StringValue2TestDtoB, source => source.StringValue5)
                .Map(dest => dest.StringValue3TestDtoB, source => source.StringValue6)
                .Map(dest => dest.DateTimeValueTestDtoB, source => source.DateTimeValue2);
            TypeAdapterConfig<SourceDto, TestDtoC>.NewConfig()
                .Map(dest => dest.DoubleValue1TestDtoC, source => source.DoubleValue7)
                .Map(dest => dest.DoubleValue2TestDtoC, source => source.DoubleValue8)
                .Map(dest => dest.DoubleValue3TestDtoC, source => source.DoubleValue9)
                .Map(dest => dest.IntValue1TestDtoC, source => source.IntValue7)
                .Map(dest => dest.IntValue2TestDtoC, source => source.IntValue8)
                .Map(dest => dest.IntValue3TestDtoC, source => source.IntValue9)
                .Map(dest => dest.StringValue1TestDtoC, source => source.StringValue7)
                .Map(dest => dest.StringValue2TestDtoC, source => source.StringValue8)
                .Map(dest => dest.StringValue3TestDtoC, source => source.StringValue9)
                .Map(dest => dest.DateTimeValueTestDtoC, source => source.DateTimeValue3);
        }

        private Mapper SetupAutoMapper()
        {
            MapperConfigurationExpression expression = new MapperConfigurationExpression();
            expression.CreateMap<SourceDto, DestinationDto>();
            expression.CreateMap<SourceDto, TestDtoA>()
                .ForMember(dest => dest.IntValue1TestDtoA, opt => opt.MapFrom(src => src.IntValue1))
                .ForMember(dest => dest.IntValue2TestDtoA, opt => opt.MapFrom(src => src.IntValue2))
                .ForMember(dest => dest.IntValue3TestDtoA, opt => opt.MapFrom(src => src.IntValue3))
                .ForMember(dest => dest.DoubleValue1TestDtoA, opt => opt.MapFrom(src => src.DoubleValue1))
                .ForMember(dest => dest.DoubleValue2TestDtoA, opt => opt.MapFrom(src => src.DoubleValue2))
                .ForMember(dest => dest.DoubleValue3TestDtoA, opt => opt.MapFrom(src => src.DoubleValue3))
                .ForMember(dest => dest.StringValue1TestDtoA, opt => opt.MapFrom(src => src.StringValue1))
                .ForMember(dest => dest.StringValue2TestDtoA, opt => opt.MapFrom(src => src.StringValue2))
                .ForMember(dest => dest.StringValue3TestDtoA, opt => opt.MapFrom(src => src.StringValue3))
                .ForMember(dest => dest.DateTimeValueTestDtoA, opt => opt.MapFrom(src => src.DateTimeValue1));
            expression.CreateMap<SourceDto, TestDtoB>()
                .ForMember(dest => dest.IntValue1TestDtoB, opt => opt.MapFrom(src => src.IntValue4))
                .ForMember(dest => dest.IntValue2TestDtoB, opt => opt.MapFrom(src => src.IntValue5))
                .ForMember(dest => dest.IntValue3TestDtoB, opt => opt.MapFrom(src => src.IntValue6))
                .ForMember(dest => dest.DoubleValue1TestDtoB, opt => opt.MapFrom(src => src.DoubleValue4))
                .ForMember(dest => dest.DoubleValue2TestDtoB, opt => opt.MapFrom(src => src.DoubleValue5))
                .ForMember(dest => dest.DoubleValue3TestDtoB, opt => opt.MapFrom(src => src.DoubleValue6))
                .ForMember(dest => dest.StringValue1TestDtoB, opt => opt.MapFrom(src => src.StringValue4))
                .ForMember(dest => dest.StringValue2TestDtoB, opt => opt.MapFrom(src => src.StringValue5))
                .ForMember(dest => dest.StringValue3TestDtoB, opt => opt.MapFrom(src => src.StringValue6))
                .ForMember(dest => dest.DateTimeValueTestDtoB, opt => opt.MapFrom(src => src.DateTimeValue2));
            expression.CreateMap<SourceDto, TestDtoC>()
                .ForMember(dest => dest.IntValue1TestDtoC, opt => opt.MapFrom(src => src.IntValue7))
                .ForMember(dest => dest.IntValue2TestDtoC, opt => opt.MapFrom(src => src.IntValue8))
                .ForMember(dest => dest.IntValue3TestDtoC, opt => opt.MapFrom(src => src.IntValue9))
                .ForMember(dest => dest.DoubleValue1TestDtoC, opt => opt.MapFrom(src => src.DoubleValue7))
                .ForMember(dest => dest.DoubleValue2TestDtoC, opt => opt.MapFrom(src => src.DoubleValue8))
                .ForMember(dest => dest.DoubleValue3TestDtoC, opt => opt.MapFrom(src => src.DoubleValue9))
                .ForMember(dest => dest.StringValue1TestDtoC, opt => opt.MapFrom(src => src.StringValue7))
                .ForMember(dest => dest.StringValue2TestDtoC, opt => opt.MapFrom(src => src.StringValue8))
                .ForMember(dest => dest.StringValue3TestDtoC, opt => opt.MapFrom(src => src.StringValue9))
                .ForMember(dest => dest.DateTimeValueTestDtoC, opt => opt.MapFrom(src => src.DateTimeValue3));

            var config = new MapperConfiguration(expression);
            return new Mapper(config);
        }
    }
}
