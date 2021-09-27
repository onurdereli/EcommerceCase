using AutoMapper;
using Dapper;
using Campaign.Models.Dtos;
using Campaign.Services.Abstract;
using Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Campaign.Models.Requests;

namespace Campaign.Services.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public CampaignService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<CampaignDto>>> GetAll()
        {
            var campaigns = await _dbConnection.QueryAsync<Models.Entities.Campaign>("Select * from campaign where threshold > 0");

            if (campaigns == null)
            {
                return Response<List<CampaignDto>>.Fail("No suitable campaigns found", 404);
            }

            return Response<List<CampaignDto>>.Success(_mapper.Map<List<CampaignDto>>(campaigns.ToList()), 200);
        }

        public async Task<Response<CampaignDto>> GetByCampaignTypeAndDiscountType(string campaignType, string discountType)
        {
            var campaign = (await _dbConnection.QueryAsync<Models.Entities.Campaign>("select * from campaign where threshold > 0 and campaigntype=@CampaignType and discounttype=@DiscountType",
                new
                {
                    CampaignType = campaignType,
                    DiscountType = discountType
                }))
                .SingleOrDefault();

            if (campaign == null)
            {
                return Response<CampaignDto>.Fail("Campaign not found", 404);
            }

            return Response<CampaignDto>.Success(_mapper.Map<CampaignDto>(campaign), 200);
        }

        public async Task<Response<CampaignDto>> GetByCampaignId(int id)
        {
            var campaign = (await _dbConnection.QueryAsync<Models.Entities.Campaign>("select * from campaign where threshold > 0 and id=@Id",
                new
                {
                    Id = id
                }))
                .SingleOrDefault();

            if (campaign == null)
            {
                return Response<CampaignDto>.Fail("Campaign not found", 404);
            }

            return Response<CampaignDto>.Success(_mapper.Map<CampaignDto>(campaign), 200);
        }

        public async Task<Response<NoContent>> Save(CampaignCreateRequest campaignCreateRequest)
        {
            var campaign = _mapper.Map<Models.Entities.Campaign>(campaignCreateRequest);

            var campaignByCampaignTypeAndDiscountType = await GetByCampaignTypeAndDiscountType(campaign.CampaignType, campaign.DiscountType);

            if(campaignByCampaignTypeAndDiscountType.IsSuccessfull)
            {
                return Response<NoContent>.Fail("The record was found with the given information", 500);
            }

            var saveStatus = await _dbConnection.ExecuteAsync("insert into campaign (campaigntype,campaignname,threshold,usedamount,discounttype,discountvalue) values (@CampaignType,@CampaignName,@Threshold,@UsedAmount,@DiscountType,@DiscountValue)", campaign);

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(201);
            }

            return Response<NoContent>.Fail("An error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(CampaignUpdateRequest campaignUpdateRequest)
        {
            var campaign = _mapper.Map<Models.Entities.Campaign>(campaignUpdateRequest);

            var updateStatus = await _dbConnection.ExecuteAsync("update campaign set campaigntype=@CampaignType, campaignname=@CampaignName, threshold=@Threshold, usedamount=@UsedAmount, discounttype=@DiscountType, discountvalue=@DiscountValue where id=@Id",
                new
                {
                    CampaignType = campaign.CampaignType,
                    CampaignName = campaign.CampaignName,
                    Threshold = campaign.Threshold,
                    UsedAmount = campaign.UsedAmount,
                    DiscountType = campaign.DiscountType,
                    DiscountValue = campaign.DiscountValue,
                    Id = campaign.Id
                });

            if (updateStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Campaign not found", 400);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("delete from campaign where id=@Id", new { Id = id });

            if (deleteStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Campaign not found", 400);
        }
    }
}